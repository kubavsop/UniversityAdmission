﻿using Admission.Application.Common.Extensions;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.ChangeApplicantData;
using Admission.User.Application.Context;
using Admission.User.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Admission.User.Application.RpcHandlers.ChangeApplicantData;

public sealed class ChangeApplicantDataRequestHandler: IRequestHandler<ChangeApplicantDataRequest, IRpcResponse>
{
    private readonly IManagerAccessService _managerAccessService;
    private readonly IUserDbContext _context;

    public ChangeApplicantDataRequestHandler(IManagerAccessService managerAccessService, IUserDbContext context)
    {
        _managerAccessService = managerAccessService;
        _context = context;
    }

    public async Task<IRpcResponse> Handle(ChangeApplicantDataRequest request, CancellationToken cancellationToken)
    {
        if (!await _managerAccessService.HasEditPermissions(request.Id, request.Role, request.ApplicantId))
            return new RpcErrorResponse
            {
                Message = "You have no rights"
            };

        var applicant = await _context.Applicants.Include(a => a.User).GetByIdAsync(request.ApplicantId);
        if (applicant == null)
            return new RpcErrorResponse
            {
                Message = "Applicant not found"
            };
        
        var modifiedNormalizedEmail = request.Email.ToUpper();
        if (await _context.Users.AnyAsync(u => request.ApplicantId != u.Id && u.NormalizedEmail == modifiedNormalizedEmail, cancellationToken: cancellationToken))
        {
            return new RpcErrorResponse("User with this email already exists");
        }
        
        applicant.User.NormalizedUserName = request.Email.ToUpper();
        applicant.User.NormalizedEmail = request.Email.ToUpper();
        applicant.ChangeFullname(request.FullName);
        applicant.ChangeEmail(request.Email);
        applicant.ChangeBirthday(request.Birthday);
        applicant.ChangeGender(request.Gender);
        applicant.ChangeCitizenship(request.Citizenship);
        applicant.ChangePhoneNumber(request.PhoneNumber);
        await _context.SaveChangesAsync(cancellationToken);

        return new RpcOkResponse();
    }
}