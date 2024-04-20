﻿// <auto-generated />
using System;
using Admission.Dictionary.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Admission.Dictionary.Infrastructure.Migrations
{
    [DbContext(typeof(DictionaryDbContext))]
    [Migration("20240416144706_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.EducationDocumentType", b =>
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

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.EducationLevel", b =>
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

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.EducationProgram", b =>
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

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.Faculty", b =>
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

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.NextEducationLevel", b =>
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

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.EducationDocumentType", b =>
                {
                    b.HasOne("Admission.Dictionary.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.EducationProgram", b =>
                {
                    b.HasOne("Admission.Dictionary.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Dictionary.Domain.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationLevel");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.NextEducationLevel", b =>
                {
                    b.HasOne("Admission.Dictionary.Domain.Entities.EducationDocumentType", "EducationDocumentType")
                        .WithMany("NextEducationLevels")
                        .HasForeignKey("EducationDocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admission.Dictionary.Domain.Entities.EducationLevel", "EducationLevel")
                        .WithMany("NextEducationLevels")
                        .HasForeignKey("EducationLevelId")
                        .HasPrincipalKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationDocumentType");

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.EducationDocumentType", b =>
                {
                    b.Navigation("NextEducationLevels");
                });

            modelBuilder.Entity("Admission.Dictionary.Domain.Entities.EducationLevel", b =>
                {
                    b.Navigation("NextEducationLevels");
                });
#pragma warning restore 612, 618
        }
    }
}
