﻿using System.Reflection;
using Admission.User.Application.Context;
using Admission.User.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Admission.User.Infrastructure;


public sealed class UserDbContext: 
    IdentityDbContext<AdmissionUser, AdmissionRole, Guid, IdentityUserClaim<Guid>, AdmissionUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>,
    IUserDbContext
{
    public DbSet<Applicant> Applicants { get; init; }
    public DbSet<Faculty> Faculties { get; init; }
    public DbSet<Manager> Managers { get; init; }
    public DbSet<StudentAdmission> StudentAdmissions { get; init; }
    
    public UserDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}