using Admission.Domain.Common.Enums;

namespace Admission.Application.Services;

public interface IManagerAccessService
{
    Task<bool> HasEditPermissions(Guid managerId, RoleType managerRole, Guid applicantId);
}