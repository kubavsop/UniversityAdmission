using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.GetApplicantData;
using Admission.User.Application.Context;
using Admission.User.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.GetApplicantData;

public sealed class GetApplicantDataRequestHandler: IRequestHandler<GetApplicantDataRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;
    private readonly IManagerAccessService _managerAccessService;

    public GetApplicantDataRequestHandler(IUserDbContext context, IManagerAccessService managerAccessService)
    {
        _context = context;
        _managerAccessService = managerAccessService;
    }

    public async Task<IRpcResponse> Handle(GetApplicantDataRequest request, CancellationToken cancellationToken)
    {
        if (request.Role == RoleType.Applicant) return new RpcErrorResponse
        {
            Message = "You have no rights"
        };

        var applicant = await _context.Applicants
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == request.ApplicantId, cancellationToken: cancellationToken);

        if (applicant == null)
        {
            return new RpcErrorResponse
            {
                Message = "Applicant not found"
            };
        }

        return new ApplicantDataResponse
        {
            ApplicantId = applicant.Id,
            Birthday = applicant.Birthday,
            Citizenship = applicant.Citizenship,
            Gender = applicant.Gender,
            Email = applicant.User.Email,
            FullName = applicant.User.FullName,
            PhoneNumber = applicant.PhoneNumber,
            IsEditable = await _managerAccessService.HasEditPermissions(request.Id, request.Role, request.ApplicantId)
        };
    }
}