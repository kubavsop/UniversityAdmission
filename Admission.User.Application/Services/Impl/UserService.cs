using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Domain.Common.Enums;
using Admission.User.Application.Context;
using Admission.User.Application.DTOs.Requests;
using Admission.User.Application.DTOs.Responses;
using Admission.User.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.Services.Impl;

public sealed class UserService : IUserService
{
    private readonly UserManager<AdmissionUser> _userManager;
    private readonly IUserDbContext _context;
    private readonly IMapper _mapper;

    public UserService(UserManager<AdmissionUser> userManager, IUserDbContext context, IMapper mapper)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ApplicantDto>> GetApplicantProfileAsync(Guid userId)
    {
        var applicant = await _context.Applicants
            .AsNoTracking()
            .Include(a => a.User)
            .Where(a => !a.User.DeleteTime.HasValue)
            .GetByIdAsync(userId);

        if (applicant == null)
        {
            return new NotFoundException(nameof(Applicant), userId);
        }

        return _mapper.Map<ApplicantDto>(applicant);
    }

    public async Task<Result> EditApplicantProfileAsync(EditApplicantDto dto, Guid userId)
    {
        if (await IsStudentAdmissionClosed(userId))
        {
            return new BadRequestException("Admission is closed");
        }

        var result = await GetApplicantByIdAsync(userId);

        if (result.IsFailure)
        {
            return result;
        }

        var applicant = result.Value;

        if (applicant.User == null)
        {
            throw new InvalidOperationException("User cannot be null");
        }

        var modifiedNormalizedEmail = dto.Email.ToUpper();

        if (modifiedNormalizedEmail != applicant.User.NormalizedEmail &&
            _context.Users.Any(u => applicant.Id != u.Id && u.NormalizedEmail == modifiedNormalizedEmail))
        {
            return new BadRequestException("User with this email already exists");
        }

        applicant.ChangeEmail(dto.Email);
        await ChangeManagerEmail(dto.Email, userId);

        applicant.ChangeFullname(dto.FullName);
        await ChangeManagerFullName(dto.FullName, userId);

        applicant.ChangeBirthday(dto.Birthday);
        applicant.ChangeGender(dto.Gender);
        applicant.ChangeCitizenship(dto.Citizenship);
        applicant.ChangePhoneNumber(dto.PhoneNumber);

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> EditPasswordAsync(EditPasswordDto dto, Guid userId)
    {
        if (dto.NewPassword == dto.OldPassword)
        {
            return new BadRequestException("Passwords must be different");
        }

        var result = await GetUserByIdAsync(userId);

        if (result.IsFailure)
        {
            return result;
        }

        var user = result.Value;
        var identityResult = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

        return identityResult.Succeeded ? Result.Success() : new IdentityException(identityResult.Errors.ToList());
    }

    private async Task ChangeManagerFullName(string fullName, Guid userId)
    {
        var manager = await _context.Managers
            .Include(m => m.User)
            .GetByIdAsync(userId);

        manager?.ChangeFullname(fullName);
    }

    private async Task ChangeManagerEmail(string email, Guid userId)
    {
        var manager = await _context.Managers
            .Include(m => m.User)
            .GetByIdAsync(userId);

        manager?.ChangeEmail(email);
    }

    private async Task<Result<Applicant>> GetApplicantByIdAsync(Guid userId)
    {
        var applicant = await _context.Applicants
            .Include(a => a.User)
            .Where(a => !a.User.DeleteTime.HasValue)
            .GetByIdAsync(userId);

        if (applicant == null)
        {
            return new NotFoundException(nameof(Applicant), userId);
        }

        return applicant;
    }

    private async Task<Result<AdmissionUser>> GetUserByIdAsync(Guid userId)
    {
        var user = await _context.Users
            .GetByIdAsync(userId);

        if (user == null)
        {
            return new NotFoundException(nameof(AdmissionUser), userId);
        }

        return user;
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