using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Application.Common.Services;
using Admission.Application.Context;
using Admission.Application.DTOs.Requests;
using Admission.Application.Options;
using Admission.Domain.Common.Enums;
using Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Admission.Application.Services.Impl;

public class ProgramService: IProgramService
{
    private const int BachelorId = 0;
    private const int SpecialtyId = 3;
    private readonly IAdmissionDbContext _context;
    private readonly IRpcDictionaryClient _dictionaryClient;
    private readonly MaximumNumberOfApplicantPrograms _maximumNumberOfApplicantPrograms;

    public ProgramService(IAdmissionDbContext context, IRpcDictionaryClient dictionaryClient, IOptions<MaximumNumberOfApplicantPrograms> maximumNumberOfApplicantPrograms)
    {
        _context = context;
        _dictionaryClient = dictionaryClient;
        _maximumNumberOfApplicantPrograms = maximumNumberOfApplicantPrograms.Value;
    }

    public async Task<Result> CreateProgramAsync(CreateProgramDto createProgramDto, Guid userId)
    {
        var admissionResult = await GetCurrentStudentAdmissionAsync(userId);

        if (admissionResult.IsFailure) return admissionResult;
        var admission = admissionResult.Value;

        if (await _context.AdmissionPrograms.Where(a => a.StudentAdmissionId == admission.Id && !a.DeleteTime.HasValue).CountAsync() >=
            _maximumNumberOfApplicantPrograms.Number)
        {
            return new BadRequestException("You already have the maximum number of programs");
        }
        
        var ensureResult = await EnsureProgramSavedAsync(createProgramDto.EducationProgramId);
        if (ensureResult.IsFailure) return ensureResult;

        var validResult = await IsValidEducationProgram(ensureResult.Value.EducationLevelId, admission);
        if (validResult.IsFailure) return validResult;
        
        if (await _context.AdmissionPrograms.AnyAsync(a => (a.Priority == createProgramDto.Priority || a.EducationProgramId == createProgramDto.EducationProgramId) && a.StudentAdmissionId == admission.Id && !a.DeleteTime.HasValue))
        {
            return new BadRequestException("Invalid request");
        }

        var isFirstPriority =
            await _context.AdmissionPrograms.AllAsync(a=>
                (a.Priority > createProgramDto.Priority && a.StudentAdmissionId == admission.Id &&
                 !a.DeleteTime.HasValue) || a.StudentAdmissionId != admission.Id || a.DeleteTime.HasValue);
        
        await _context.AdmissionPrograms.AddAsync(AdmissionProgram.Create(admission.Id,
            ensureResult.Value, createProgramDto.Priority, isFirstPriority));
        
        admission.ChangeStatus(admission.ManagerId != null
            ? AdmissionStatus.UnderReview
            : AdmissionStatus.Created);

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteProgramAsync(Guid programId, Guid userId)
    {
        var programResult = await GetProgramById(programId, userId);
        if (programResult.IsFailure) return programResult;

        var program = programResult.Value;
        program.DeleteTime = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Result.Success();
    }
    
    public async Task<Result> EditProgramsAsync(EditProgramsDto editProgramDto, Guid userId)
    {
        var editProgramsDto = editProgramDto.EditPrograms.ToList();
        var admission = await _context.StudentAdmissions.FirstOrDefaultAsync(sa =>
            sa.ApplicantId == userId && sa.Status != AdmissionStatus.Closed);

        if (admission == null)
        {
            return new BadRequestException("Could not find the applicant's current admission");
        }

        var programs = await _context.AdmissionPrograms
            .Include(p => p.EducationProgram)
            .ThenInclude(ep => ep.Faculty)
            .Where(a => a.StudentAdmissionId == admission.Id && !a.DeleteTime.HasValue)
            .ToListAsync();
        
        var priorityDictionary = programs.ToDictionary(p => p.Priority);
        var idDictionary = programs.ToDictionary(p => p.Id);
        
        var uniqueResult = IsUniquePriorities(editProgramsDto, idDictionary);
        if (uniqueResult.IsFailure) return uniqueResult;
        
        var priorityDictionaryDto = editProgramsDto.ToDictionary(p => p.Priority);
        var idDictionaryDto = editProgramsDto.ToDictionary(p => p.AdmissionProgramId);

        var availableResult = IsPrioritiesAvailable(idDictionaryDto, priorityDictionary);
        if (availableResult.IsFailure) return availableResult;

        var firstPriority = GetFirstPriority(priorityDictionary, priorityDictionaryDto, idDictionaryDto);

        foreach (var program in editProgramsDto)
        {
            idDictionary.TryGetValue(program.AdmissionProgramId, out var p);
            p!.ChangePriority(program.Priority, program.Priority == firstPriority);
        }

        await _context.SaveChangesAsync();
        return Result.Success();
    }
    
    private Result IsUniquePriorities(IEnumerable<EditProgramDto> editProgramsDto, Dictionary<Guid, AdmissionProgram> idDictionary)
    {
        var testHashSet = new HashSet<int>();
        foreach (var dto in editProgramsDto)
        {
            if (!testHashSet.Add(dto.Priority))
            {
                return new BadRequestException("Priorities must be unique");
            }

            if (!idDictionary.ContainsKey(dto.AdmissionProgramId))
            {
                return new NotFoundException(nameof(AdmissionProgram), dto.AdmissionProgramId);
            }
        }

        return Result.Success();
    }

    private Result IsPrioritiesAvailable(Dictionary<Guid, EditProgramDto> dtoDictionary, Dictionary<int, AdmissionProgram> priorityDictionary)
    {
        foreach (var dto in dtoDictionary.Values)
        {
            if (priorityDictionary.TryGetValue(dto.Priority, out var program) && !dtoDictionary.ContainsKey(program.Id))
            {
                return new BadRequestException($"The priority '{dto.Priority}' already exists'");
            }
        }

        return Result.Success();
    }

    private int GetFirstPriority(
        Dictionary<int, AdmissionProgram> priorityDictionary, 
        Dictionary<int , EditProgramDto> dtoPriorityDictionary, 
        Dictionary<Guid, EditProgramDto> dtoDictionary)
    {
        int maxPriority = dtoPriorityDictionary.Keys.Min();
        foreach (var program in priorityDictionary.Values)
        {
            if (program.Priority < maxPriority && !dtoDictionary.ContainsKey(program.Id))
            {
                maxPriority = program.Priority;
            }
        }

        return maxPriority;
    }

    private async Task<Result<EducationProgram>> EnsureProgramSavedAsync(Guid programId)
    {
        var program = await _context.EducationPrograms
            .Include(e => e.Faculty)
            .GetByIdAsync(programId);
        if (program != null) return program;

        var programResponse = await _dictionaryClient.GetProgramByIdAsync(programId);

        if (programResponse == null)
        {
            return new NotFoundException(nameof(EducationProgram), programId);
        }

        if (!await _context.EducationLevels.AnyAsync(l => l.ExternalId == programResponse.EducationLevel.ExternalId))
        {
            await _context.EducationLevels.AddAsync(new EducationLevel
            {
                Id = programResponse.EducationLevel.Id,
                ExternalId = programResponse.EducationLevel.ExternalId,
                Name = programResponse.EducationLevel.Name
            });
        }

        if (!await _context.Faculties.AnyAsync(f => f.Id == programResponse.Faculty.Id))
        {
            await _context.Faculties.AddAsync(new Faculty
            {
                Id = programResponse.Faculty.Id,
                Name = programResponse.Faculty.Name
            });
        }

        var educationProgram = new EducationProgram
        {
            Id = programId,
            Name = programResponse.Name,
            Code = programResponse.Code,
            Language = programResponse.Language,
            EducationForm = programResponse.EducationForm,
            FacultyId = programResponse.Faculty.Id,
            EducationLevelId = programResponse.EducationLevel.ExternalId
        };
        
        await _context.EducationPrograms.AddAsync(educationProgram);

        return educationProgram;
    }

    private async Task<Result<StudentAdmission>> GetCurrentStudentAdmissionAsync(Guid userId)
    {
        var admission =
            await _context.StudentAdmissions
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(sa =>
                sa.Status != AdmissionStatus.Closed && sa.ApplicantId == userId);

        if (admission == null)
        {
            return new BadRequestException("You cannot select a program for admission");
        }

        return admission;
    }

    private async Task<Result<AdmissionProgram>> GetProgramById(Guid programId, Guid userId)
    {
        var program = await _context.AdmissionPrograms
            .Include(p => p.StudentAdmission)
            .GetByIdAsync(programId);
        
        if (program == null)
        {
            return new NotFoundException(nameof(program), programId);
        }

        if (program.StudentAdmission.ApplicantId != userId)
        {
            return new ForbiddenException(userId);
        }

        if (program.StudentAdmission.Status == AdmissionStatus.Closed)
        {
            return new BadRequestException("Student admission is closed");
        }

        return program;
    }

    private async Task<Result> IsValidEducationProgram(int externalLevelId, StudentAdmission currentAdmission)
    {
        var documentTypes = await _context.EducationDocuments
            .AsNoTracking()
            .Where(d => !d.DeleteTime.HasValue && d.ApplicantId == currentAdmission.ApplicantId)
            .Include(t => t.EducationDocumentType)
            .ThenInclude(d => d.NextEducationLevels)
            .Select(d => d.EducationDocumentType)
            .ToListAsync();

        if (documentTypes
            .All(t => t.EducationLevelId != externalLevelId && !t.DeleteTime.HasValue && t.NextEducationLevels
                                       .All(nl => nl.EducationLevelId != externalLevelId && !nl.DeleteTime.HasValue)))
        {
            return new BadRequestException("Unacceptable level of education");
        }

        var admissionProgram = await _context.AdmissionPrograms
            .Include(ap => ap.EducationProgram)
            .FirstOrDefaultAsync(ap => !ap.DeleteTime.HasValue && ap.StudentAdmissionId == currentAdmission.Id);
        if (admissionProgram == null) return Result.Success();

        if (admissionProgram.EducationProgram.EducationLevelId != externalLevelId &&
            !(admissionProgram.EducationProgram.EducationLevelId == SpecialtyId && externalLevelId == BachelorId) &&
            !(admissionProgram.EducationProgram.EducationLevelId == BachelorId && externalLevelId == SpecialtyId))
        {
            return new BadRequestException("You cannot choose directions of different levels");
        }
        
        return Result.Success();
    }
}