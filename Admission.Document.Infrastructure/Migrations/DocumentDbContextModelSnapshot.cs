﻿// <auto-generated />
using System;
using Admission.Document.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Admission.Document.Infrastructure.Migrations
{
    [DbContext(typeof(DocumentDbContext))]
    partial class DocumentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Admission.Document.Domain.Entities.Applicant", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Documents");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.EducationDocumentType", b =>
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("EducationDocumentTypes");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.EducationLevel", b =>
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

            modelBuilder.Entity("Admission.Document.Domain.Entities.Faculty", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("FacultyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.NextEducationLevel", b =>
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

            modelBuilder.Entity("Admission.Document.Domain.Entities.StudentAdmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("FirstPriorityFacultyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

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

            modelBuilder.Entity("Admission.Document.Domain.Entities.EducationDocument", b =>
                {
                    b.HasBaseType("Admission.Document.Domain.Entities.Document");

                    b.Property<Guid>("EducationDocumentTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("EducationDocumentTypeId");

                    b.ToTable("EducationDocuments");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Passport", b =>
                {
                    b.HasBaseType("Admission.Document.Domain.Entities.Document");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IssuedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("PlaceOfBirth")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Series")
                        .HasColumnType("integer");

                    b.ToTable("Passports");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Document", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.EducationDocumentType", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.File", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.Document", null)
                        .WithMany("Files")
                        .HasForeignKey("DocumentId");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Manager", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.NextEducationLevel", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.EducationDocumentType", "EducationDocumentType")
                        .WithMany("NextEducationLevels")
                        .HasForeignKey("EducationDocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Document.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany("NextEducationLevels")
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationDocumentType");

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.StudentAdmission", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.Applicant", "Applicant")
                        .WithMany("Admissions")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Document.Domain.Entities.Manager", "Manager")
                        .WithMany("Admissions")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Applicant");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.EducationDocument", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.EducationDocumentType", "EducationDocumentType")
                        .WithMany()
                        .HasForeignKey("EducationDocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Document.Domain.Entities.Document", null)
                        .WithOne()
                        .HasForeignKey("Admission.Document.Domain.Entities.EducationDocument", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationDocumentType");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Passport", b =>
                {
                    b.HasOne("Admission.Document.Domain.Entities.Document", null)
                        .WithOne()
                        .HasForeignKey("Admission.Document.Domain.Entities.Passport", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Applicant", b =>
                {
                    b.Navigation("Admissions");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Document", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.EducationDocumentType", b =>
                {
                    b.Navigation("NextEducationLevels");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.EducationLevel", b =>
                {
                    b.Navigation("NextEducationLevels");
                });

            modelBuilder.Entity("Admission.Document.Domain.Entities.Manager", b =>
                {
                    b.Navigation("Admissions");
                });
#pragma warning restore 612, 618
        }
    }
}
