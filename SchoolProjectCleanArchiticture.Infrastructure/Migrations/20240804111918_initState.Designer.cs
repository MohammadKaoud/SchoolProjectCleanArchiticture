﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolProjectCleanArchiticture.Data;

#nullable disable

namespace SchoolProjectCleanArchiticture.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240804111918_initState")]
    partial class initState
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartmentSubject", b =>
                {
                    b.Property<int>("DepartmentsDepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsSubjectID")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsDepartmentId", "SubjectsSubjectID");

                    b.HasIndex("SubjectsSubjectID");

                    b.ToTable("DepartmentSubject");
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<int>("DeparmentManagerId")
                        .HasColumnType("int");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("DepartmentId");

                    b.ToTable("departments");

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            DeparmentManagerId = 0,
                            DepartmentName = "InformaticEng"
                        },
                        new
                        {
                            DepartmentId = 2,
                            DeparmentManagerId = 0,
                            DepartmentName = "Dentistry"
                        });
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("NameAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Mazzeh",
                            DepartmentId = 2,
                            NameAr = "محمد",
                            NameEn = "Mohammad",
                            Phone = "21215454"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Mazzeh",
                            DepartmentId = 1,
                            NameAr = "عمر",
                            NameEn = "Omar",
                            Phone = "21211111"
                        });
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectID"));

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("TimeOfPeriod")
                        .HasColumnType("int");

                    b.HasKey("SubjectID");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AdvisorId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("ManagedDepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AdvisorId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ManagedDepartmentId")
                        .IsUnique()
                        .HasFilter("[ManagedDepartmentId] IS NOT NULL");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsSubjectID")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "SubjectsSubjectID");

                    b.HasIndex("SubjectsSubjectID");

                    b.ToTable("StudentSubject");
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.Property<int>("SubjectsSubjectID")
                        .HasColumnType("int");

                    b.Property<int>("TeahersId")
                        .HasColumnType("int");

                    b.HasKey("SubjectsSubjectID", "TeahersId");

                    b.HasIndex("TeahersId");

                    b.ToTable("SubjectTeacher");
                });

            modelBuilder.Entity("DepartmentSubject", b =>
                {
                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Student", b =>
                {
                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Teacher", b =>
                {
                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Teacher", "Advisor")
                        .WithMany("SubordinateTeachers")
                        .HasForeignKey("AdvisorId");

                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Department", "ManagedDepartment")
                        .WithOne("Manager")
                        .HasForeignKey("SchoolProjectCleanArchiticture.Data.Entites.Teacher", "ManagedDepartmentId");

                    b.Navigation("Advisor");

                    b.Navigation("Department");

                    b.Navigation("ManagedDepartment");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProjectCleanArchiticture.Data.Entites.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeahersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Department", b =>
                {
                    b.Navigation("Manager")
                        .IsRequired();

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("SchoolProjectCleanArchiticture.Data.Entites.Teacher", b =>
                {
                    b.Navigation("SubordinateTeachers");
                });
#pragma warning restore 612, 618
        }
    }
}
