using Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Context;

public interface IAdmissionDbContext
{
    public DbSet<AdmissionProgram> AdmissionPrograms { get; }
    public DbSet<Applicant> Applicants { get; }
    public DbSet<EducationLevel> EducationLevels { get; }
    public DbSet<EducationProgram> EducationPrograms { get; }
    public DbSet<Faculty> Faculties { get; }
    public DbSet<Manager> Managers { get; }
    public DbSet<NextEducationLevel> NextEducationLevels { get; }
    public DbSet<StudentAdmission> StudentAdmissions { get; }
    public DbSet<AdmissionGroup> AdmissionGroups { get; }
}