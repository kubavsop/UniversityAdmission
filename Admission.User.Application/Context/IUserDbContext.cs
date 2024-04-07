using Admission.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Application.Context;

public interface IUserDbContext
{
    DbSet<AdmissionUser> Users { get; }
    DbSet<Applicant> Applicants { get; }
    DbSet<Faculty> Faculties { get;  }
    DbSet<Manager> Managers { get; }
    DbSet<StudentAdmission> StudentAdmissions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}