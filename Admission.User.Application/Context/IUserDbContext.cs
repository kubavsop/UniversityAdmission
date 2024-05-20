using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.Context;

public interface IUserDbContext
{
    DbSet<AdmissionRole> Roles { get; }
    DbSet<AdmissionUserRole> UserRoles { get; }
    DbSet<AdmissionUser> Users { get; }
    DbSet<Applicant> Applicants { get; }
    DbSet<Faculty> Faculties { get;  }
    DbSet<Manager> Managers { get; }
    DbSet<StudentAdmission> StudentAdmissions { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}