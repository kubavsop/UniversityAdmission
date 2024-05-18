using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Application.Common.Services;
using Admission.Document.Application.Constants;
using Admission.Document.Application.Context;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;
using Admission.Document.Domain.Entities;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.EducationLevel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using File = Admission.Document.Domain.Entities.File;

namespace Admission.Document.Application.Services.Impl;

public sealed class DocumentService : IDocumentService
{
    private readonly IDocumentDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileProvider _fileProvider;
    private readonly IRpcDictionaryClient _dictionaryClient;

    public DocumentService(IDocumentDbContext context, IMapper mapper, IRpcDictionaryClient dictionaryClient, IFileProvider fileProvider)
    {
        _context = context;
        _mapper = mapper;
        _dictionaryClient = dictionaryClient;
        _fileProvider = fileProvider;
    }

    public async Task<Result> CreatePassportAsync(CreatePassportDto passportDto, Guid userId)
    {
        if (await IsStudentAdmissionClosed(userId))
        {
            return new BadRequestException("Admission is closed");
        }

        if (_context.Passports.GetUndeleted().Any(p => p.ApplicantId == userId))
        {
            return new BadRequestException("Passport already exists");
        }

        await _context.Passports.AddAsync(Passport.CreatePassport(
            passportDto.Series,
            passportDto.Number,
            passportDto.PlaceOfBirth,
            passportDto.IssuedBy,
            passportDto.DateIssued,
            userId));

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<PassportDto>> GetPassportAsync(Guid userId)
    {
        var passport = await _context.Passports
            .Include(p => p.Files.Where(f => !f.DeleteTime.HasValue))
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ApplicantId == userId);

        if (passport == null)
        {
            return new NotFoundException("Passport was not found");
        }

        return _mapper.Map<PassportDto>(passport);
    }

    public async Task<Result> EditPassportAsync(EditPassportDto passportDto, Guid userId)
    {
        if (await IsStudentAdmissionClosed(userId))
        {
            return new BadRequestException("Admission is closed");
        }

        var passport = await _context.Passports
            .GetUndeleted()
            .FirstOrDefaultAsync(p => p.ApplicantId == userId);

        if (passport == null)
        {
            return new NotFoundException("Passport was not found");
        }

        passport.ChangeSeries(passportDto.Series);
        passport.ChangeNumber(passportDto.Number);
        passport.ChangePlaceOfBirth(passportDto.PlaceOfBirth);
        passport.ChangeDateIssued(passportDto.DateIssued);
        passport.ChangeIssuedBy(passportDto.IssuedBy);

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> CreateEducationDocumentAsync(CreateEducationDocumentDto documentDto, Guid userId)
    {
        if (await IsStudentAdmissionClosed(userId))
        {
            return new BadRequestException("Admission is closed");
        }

        var resultExistenceCheck = await EducationDocumentExistenceCheck(userId, documentDto.EducationDocumentTypeId);
        if (resultExistenceCheck.IsFailure) return resultExistenceCheck.Exception;

        var result = await EnsureDocumentTypeSaved(documentDto.EducationDocumentTypeId);
        if (result.IsFailure) return result.Exception;

        await _context.EducationDocuments.AddAsync(EducationDocument.Create(documentDto.Name,
            documentDto.EducationDocumentTypeId, userId));

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<EducationDocumentDto>>> GetEducationDocumentAsync(Guid userId)
    {
        var documents = await _context.EducationDocuments
            .AsNoTracking()
            .GetUndeleted()
            .Include(p => p.Files.Where(f => !f.DeleteTime.HasValue))
            .Where(d => d.ApplicantId == userId)
            .Where(d => !d.EducationDocumentType.DeleteTime.HasValue)
            .Include(d => d.EducationDocumentType)
            .ThenInclude(t => t.NextEducationLevels
                .Where(nel => !nel.DeleteTime.HasValue))
            .ThenInclude(nel => nel.EducationLevel)
            .Include(d => d.EducationDocumentType)
            .ThenInclude(d => d.EducationLevel)
            .ToListAsync();

        return _mapper.Map<List<EducationDocumentDto>>(documents);
    }

    public async Task<Result> EditEducationDocumentAsync(EditEducationDocumentDto documentDto, Guid documentId,
        Guid userId)
    {
        var document = await _context.EducationDocuments.GetByIdAsync(documentId);
        
        if (document == null)
        {
            return new NotFoundException(nameof(EducationDocument), documentId);
        }

        if (document.EducationDocumentTypeId != documentDto.EducationDocumentTypeId)
        {
            var result = await EducationDocumentExistenceCheck(userId, documentDto.EducationDocumentTypeId);
            if (result.IsFailure) return result.Exception;
        }

        if (document.ApplicantId != userId)
        {
            return new ForbiddenException(userId);
        }

        if (document.EducationDocumentTypeId != documentDto.EducationDocumentTypeId)
        {
            var result = await EnsureDocumentTypeSaved(documentDto.EducationDocumentTypeId);
            if (result.IsFailure) return result.Exception;
        }

        document.ChangeName(documentDto.Name);
        document.ChangeDocumentType(documentDto.EducationDocumentTypeId);

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteEducationDocumentAsync(Guid documentId, Guid userId)
    {
        var document = await _context.EducationDocuments.GetByIdAsync(documentId);

        if (document == null)
        {
            return new NotFoundException(nameof(EducationDocument), documentId);
        }

        if (document.ApplicantId != userId)
        {
            return new ForbiddenException(userId);
        }

        document.ChangeDeleteTime(DateTime.UtcNow);

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> AddScan(Guid userId, Guid documentId, CreateScanDto createScanDto)
    {
        var educationDocument = await _context.Documents.GetByIdAsync(documentId);

        if (educationDocument == null)
        {
            return new NotFoundException(nameof(Document), documentId);
        }

        if (educationDocument.ApplicantId != userId)
        {
            return new ForbiddenException(userId);
        }
        
        byte[] file;
        using (var stream = new MemoryStream())
        {
            await createScanDto.File.CopyToAsync(stream);
            file = stream.ToArray();   
        }

        var fileId = Guid.NewGuid();
        
        await _fileProvider.PutFileAsync(fileId, file);
        
        await _context.Files.AddAsync(File.Create(
            fileId,
            documentId,
            Path.GetFileNameWithoutExtension(createScanDto.File.FileName),
            ContentTypeMappings.TypeMappings[Path.GetExtension(createScanDto.File.FileName)],
            createScanDto.File.Length,
           educationDocument.ApplicantId));

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    private async Task<Result> EducationDocumentExistenceCheck(Guid userId, Guid documentType)
    {
        if (await _context.EducationDocuments.AnyAsync(e => e.EducationDocumentTypeId == documentType && userId == e.ApplicantId && !e.DeleteTime.HasValue))
        {
            return new BadRequestException("Document already exists");
        }

        return Result.Success();
    }

    private async Task<Result> EnsureDocumentTypeSaved(Guid typeId)
    {
        var type = await _context.EducationDocumentTypes.FirstOrDefaultAsync(t =>
            t.Id == typeId);

        if (type is { DeleteTime: not null })
        {
            return new BadRequestException("Type has been deleted");
        }

        if (type != null) return Result.Success();

        var documentType = await _dictionaryClient.GetDocumentTypeByIdAsync(typeId);

        if (documentType == null)
        {
            return new NotFoundException(nameof(EducationDocumentType), typeId);
        }

        var educationLevelsDict = await _context.EducationLevels
            .AsNoTracking()
            .GetUndeleted()
            .ToDictionaryAsync(l => l.ExternalId);

        if (!educationLevelsDict.ContainsKey(documentType.EducationLevel.ExternalId))
        {
            await SaveEducationLevel(documentType.EducationLevel);
        }

        foreach (var educationLevel in documentType.NextEducationLevels)
        {
            if (!educationLevelsDict.ContainsKey(educationLevel.ExternalId))
            {
                await SaveEducationLevel(educationLevel);
            }

            await _context.NextEducationLevels.AddAsync(new NextEducationLevel
            {
                EducationDocumentTypeId = typeId,
                EducationLevelId = educationLevel.ExternalId
            });
        }


        await _context.EducationDocumentTypes.AddAsync(new EducationDocumentType
        {
            Id = documentType.Id,
            Name = documentType.Name,
            EducationLevelId = documentType.EducationLevel.ExternalId,
        });

        return Result.Success();
    }

    private async Task SaveEducationLevel(EducationLevelResponse educationLevelResponse)
    {
        var educationLevel = new EducationLevel
        {
            Id = educationLevelResponse.Id,
            Name = educationLevelResponse.Name,
            ExternalId = educationLevelResponse.ExternalId
        };

        await _context.EducationLevels.AddAsync(educationLevel);
    }

    private async Task<bool> IsStudentAdmissionClosed(Guid userId)
    {
        var admissions = await _context.StudentAdmissions
            .GetUndeleted()
            .Where(sa => sa.ApplicantId == userId)
            .ToListAsync();

        return admissions.Count != 0 && !admissions.Any(
            sa => sa.ApplicantId == userId && sa.Status != AdmissionStatus.Closed);
    }
}