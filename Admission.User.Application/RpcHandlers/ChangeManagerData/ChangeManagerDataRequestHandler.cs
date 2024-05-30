using Admission.Application.Common.Extensions;
using Admission.Application.Common.Services;
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
    private readonly IRpcDictionaryClient _dictionaryClient;

    public ChangeManagerDataRequestHandler(IUserDbContext context, IRpcDictionaryClient dictionaryClient)
    {
        _context = context;
        _dictionaryClient = dictionaryClient;
    }
    
    public async Task<IRpcResponse> Handle(ChangeManagerDataRequest request, CancellationToken cancellationToken)
    {
        if (request.Id != request.ManagerId && request.Role != RoleType.Admin)
            return new RpcErrorResponse("You have no rights");

        if (request.Id == request.ManagerId && request.Role != RoleType.Admin && request.FacultyId != null)
        {
            return new RpcErrorResponse("You have no rights");
        }
        
        var modifiedNormalizedEmail = request.Email.ToUpper();
        if (await _context.Users.AnyAsync(u => request.ManagerId != u.Id && u.NormalizedEmail == modifiedNormalizedEmail, cancellationToken: cancellationToken))
        {
            return new RpcErrorResponse("User with this email already exists");
        }

        var manager = await _context.Managers.Include(u => u.User).GetByIdAsync(request.ManagerId);
        if (manager == null) return new RpcErrorResponse("Manager not found");

        if (request.FacultyId != null)
        {
            var faculty = await _context.Faculties.GetByIdAsync(request.FacultyId.Value);
            if (faculty == null)
            {
                var facultyResponse = await _dictionaryClient.GetFacultyByIdAsync(request.FacultyId.Value);
                faculty = new Faculty
                {
                    Id = facultyResponse!.Id,
                    Name = facultyResponse.Name
                };
                await _context.Faculties.AddAsync(faculty, cancellationToken);
            }
            manager.ChangeFaculty(faculty);
        }
        else
        {
            manager.ChangeFaculty(null);
        }
        
        manager.User.NormalizedUserName = request.Email.ToUpper();
        manager.User.NormalizedEmail = request.Email.ToUpper();
        manager.ChangeFullname(request.FullName);
        manager.ChangeEmail(request.Email);

        await _context.SaveChangesAsync(cancellationToken);
        return new RpcOkResponse();
    }
}