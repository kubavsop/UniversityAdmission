﻿// <auto-generated />
using System;
using Admission.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Admission.Infrastructure.Migrations
{
    [DbContext(typeof(AdmissionDbContext))]
    partial class AdmissionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Admission.Domain.Entities.AdmissionGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AdmissionGroups");
                });

            modelBuilder.Entity("Admission.Domain.Entities.AdmissionProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EducationProgramId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<Guid>("StudentAdmissionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EducationProgramId");

                    b.HasIndex("StudentAdmissionId");

                    b.ToTable("AdmissionPrograms");
                });

            modelBuilder.Entity("Admission.Domain.Entities.Applicant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationDocumentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EducationLevelId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("EducationDocumentTypes");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EducationLevels");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EducationForm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EducationLevelId")
                        .HasColumnType("integer");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EducationLevelId");

                    b.HasIndex("FacultyId");

                    b.ToTable("EducationPrograms");
                });

            modelBuilder.Entity("Admission.Domain.Entities.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Admission.Domain.Entities.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("FacultyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Admission.Domain.Entities.NextEducationLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EducationDocumentTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("EducationLevelId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EducationDocumentTypeId");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("NextEducationLevels");
                });

            modelBuilder.Entity("Admission.Domain.Entities.StudentAdmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdmissionGroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AdmissionGroupId");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("ManagerId");

                    b.ToTable("StudentAdmissions");
                });

            modelBuilder.Entity("Admission.OutboxMessages.OutboxMessages.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OccurredTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("Admission.Domain.Entities.AdmissionProgram", b =>
                {
                    b.HasOne("Admission.Domain.Entities.EducationProgram", "EducationProgram")
                        .WithMany("AdmissionPrograms")
                        .HasForeignKey("EducationProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Domain.Entities.StudentAdmission", "StudentAdmission")
                        .WithMany("AdmissionPrograms")
                        .HasForeignKey("StudentAdmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationProgram");

                    b.Navigation("StudentAdmission");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationDocumentType", b =>
                {
                    b.HasOne("Admission.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationProgram", b =>
                {
                    b.HasOne("Admission.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Domain.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Admission.Domain.Entities.Manager", b =>
                {
                    b.HasOne("Admission.Domain.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Admission.Domain.Entities.NextEducationLevel", b =>
                {
                    b.HasOne("Admission.Domain.Entities.EducationDocumentType", "EducationDocumentType")
                        .WithMany("NextEducationLevels")
                        .HasForeignKey("EducationDocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany("NextEducationLevels")
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationDocumentType");

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Admission.Domain.Entities.StudentAdmission", b =>
                {
                    b.HasOne("Admission.Domain.Entities.AdmissionGroup", "AdmissionGroup")
                        .WithMany("Admissions")
                        .HasForeignKey("AdmissionGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Domain.Entities.Applicant", "Applicant")
                        .WithMany("Admissions")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Domain.Entities.Manager", "Manager")
                        .WithMany("Admissions")
                        .HasForeignKey("ManagerId");

                    b.Navigation("AdmissionGroup");

                    b.Navigation("Applicant");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Admission.Domain.Entities.AdmissionGroup", b =>
                {
                    b.Navigation("Admissions");
                });

            modelBuilder.Entity("Admission.Domain.Entities.Applicant", b =>
                {
                    b.Navigation("Admissions");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationDocumentType", b =>
                {
                    b.Navigation("NextEducationLevels");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationLevel", b =>
                {
                    b.Navigation("NextEducationLevels");
                });

            modelBuilder.Entity("Admission.Domain.Entities.EducationProgram", b =>
                {
                    b.Navigation("AdmissionPrograms");
                });

            modelBuilder.Entity("Admission.Domain.Entities.Manager", b =>
                {
                    b.Navigation("Admissions");
                });

            modelBuilder.Entity("Admission.Domain.Entities.StudentAdmission", b =>
                {
                    b.Navigation("AdmissionPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
