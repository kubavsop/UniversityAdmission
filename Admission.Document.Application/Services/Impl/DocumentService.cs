using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Document.Application.Context;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;
using Admission.Document.Domain.Entities;
using Admission.Document.Domain.Enums;
using Admission.Domain.Common.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.Services.Impl;

public sealed class DocumentService: IDocumentService
{
    private readonly IDocumentDbContext _context;
    private readonly IMapper _mapper;

    public DocumentService(IDocumentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
            .Include(p => p.Files)
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