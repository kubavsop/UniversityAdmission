using Admission.Application.Context;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.AdmissionService.GetAdmissionGroups;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.RpcHandlers;

public sealed class GetAdmissionsGroupsRequestHandler: IRequestHandler<GetAdmissionGroupsRequest, IRpcResponse>
{
    private readonly IAdmissionDbContext _context;

    public GetAdmissionsGroupsRequestHandler(IAdmissionDbContext context)
    {
        _context = context;
    }

    public async Task<IRpcResponse> Handle(GetAdmissionGroupsRequest request, CancellationToken cancellationToken)
    {
        var groups = await _context.AdmissionGroups
            .AsNoTracking()
            .Select(g => new AdmissionGroupResponse
        {
            Id = g.Id,
            CreateTime = g.CreateTime,
            Status = g.Status
        }).ToListAsync(cancellationToken: cancellationToken);

        return new AdmissionGroupsResponse
        {
            Groups = groups
        };
    }
}