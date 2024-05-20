using Admission.Application.Common.Extensions;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.UserService.ChangeManagerData;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.RpcHandlers.ChangeManagerData;

public sealed class ChangeManagerDataRequestHandler: IRequestHandler<ChangeManagerDataRequest, IRpcResponse>
{
    private readonly IUserDbContext _context;

    public ChangeManagerDataRequestHandler(IUserDbContext context)
    {
        _context = context;
    }
    
    public async Task<IRpcResponse> Handle(ChangeManagerDataRequest request, CancellationToken cancellationToken)
    {
        if (request.Id != request.ManagerId && request.Role != RoleType.Admin)
            return new RpcErrorResponse("You have no rights");

        var manager = await _context.Managers.Include(u => u.User).GetByIdAsync(request.ManagerId);
        if (manager == null) return new RpcErrorResponse("Manager not found");

        if (request.FacultyId != null)
        {
            var faculty = await _context.Faculties.GetByIdAsync(request.FacultyId.Value);
            if (faculty == null)
            {
                faculty = new Faculty
                {
                    Id = request.FacultyId.Value,
                    Name = request.FacultyName!
                };
                await _context.Faculties.AddAsync(faculty, cancellationToken);
            }
            manager.ChangeFaculty(faculty);
        }
        else
        {
            manager.ChangeFaculty(null);
        }
        
        manager.ChangeFullname(request.FullName);
        manager.ChangeEmail(request.Email);

        await _context.SaveChangesAsync(cancellationToken);
        return new RpcOkResponse();
    }
}