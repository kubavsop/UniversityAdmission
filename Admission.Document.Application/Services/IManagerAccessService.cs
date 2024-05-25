using Admission.Document.Domain.Entities;
using Admission.Domain.Common.Enums;

namespace Admission.Document.Application.Services;

public interface IManagerAccessService
{
    Task<bool> HasEditPermissions(Guid managerId, RoleType managerRole, Guid applicantId);
    bool HasEditPermissions(Manager? manager, RoleType managerRole, StudentAdmission? studentAdmission);
}