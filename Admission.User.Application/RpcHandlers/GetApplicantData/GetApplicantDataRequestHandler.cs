using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.GetApplicantData;
using Admission.User.Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.GetApplicantData;

public sealed class GetApplicantDataRequestHandler: IRequestHandler<GetApplicantDataRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public GetApplicantDataRequestHandler(IUserDbContext context)
    {
        _context = context;
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
            Email = applicant.User.Email,
            FullName = applicant.User.FullName,
            PhoneNumber = applicant.PhoneNumber
        };
    }
}