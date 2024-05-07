using Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admission.Application.Context;

public interface IAdmissionDbContext
{
    DbSet<EducationDocumentType> EducationDocumentTypes { get; }
    DbSet<AdmissionProgram> AdmissionPrograms { get; }
    DbSet<Applicant> Applicants { get; }
    DbSet<EducationLevel> EducationLevels { get; }
    DbSet<EducationProgram> EducationPrograms { get; }
    DbSet<Faculty> Faculties { get; }
    DbSet<Manager> Managers { get; }
    DbSet<NextEducationLevel> NextEducationLevels { get; }
    DbSet<StudentAdmission> StudentAdmissions { get; }
    DbSet<AdmissionGroup> AdmissionGroups { get; }
}