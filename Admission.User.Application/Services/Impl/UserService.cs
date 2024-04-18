using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.User.Application.Context;
using Admission.User.Application.DTOs.Requests;
using Admission.User.Application.DTOs.Responses;
using Admission.User.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.Services.Impl;

public sealed class UserService: IUserService
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
       
       applicant.User.FullName = dto.FullName;
       applicant.User.Email = dto.Email;
       applicant.Birthday = dto.Birthday;
       applicant.Gender = dto.Gender;
       applicant.Citizenship = dto.Citizenship;
       applicant.PhoneNumber = dto.PhoneNumber;

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
}