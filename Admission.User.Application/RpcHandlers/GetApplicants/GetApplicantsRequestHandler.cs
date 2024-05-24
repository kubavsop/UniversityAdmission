using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.GetApplicants;
using Admission.User.Application.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.GetApplicants;

public sealed class GetApplicantsRequestHandler: IRequestHandler<GetApplicantsRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public GetApplicantsRequestHandler(IUserDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(GetApplicantsRequest request, CancellationToken cancellationToken)
    {
        if (request.Role == RoleType.Applicant) return new RpcErrorResponse
        {
            Message = "You have no rights"
        };

        var users = await _context.Users
            .Where(u => u.UserRoles.Count == 1)
            .Select(u => new ShortApplicantResponse {
            ApplicantId = u.Id,
            Email = u.Email,
            FullName = u.FullName
        }).ToListAsync(cancellationToken: cancellationToken);

        return new ApplicantsResponse
        {
            Applicants = users
        };
    }
}