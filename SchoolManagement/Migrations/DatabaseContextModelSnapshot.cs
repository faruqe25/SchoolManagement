﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolManagement.Database;

namespace SchoolManagement.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.Class", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName");

                    b.HasKey("ClassId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.ExamType", b =>
                {
                    b.Property<int>("ExamTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExamName");

                    b.HasKey("ExamTypeId");

                    b.ToTable("ExamType");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.Login", b =>
                {
                    b.Property<long>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ActiveStatus");

                    b.Property<string>("CurrentPassword");

                    b.Property<long>("DistinguishId");

                    b.Property<bool>("FirstLoginStatus");

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.Property<string>("Username");

                    b.HasKey("LoginId");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.Section", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SectionName");

                    b.HasKey("SectionId");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.SectionCapacity", b =>
                {
                    b.Property<int>("SectionCapacityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<int>("ClassId");

                    b.Property<int>("SectionId");

                    b.Property<long>("SectionYear");

                    b.HasKey("SectionCapacityId");

                    b.HasIndex("ClassId");

                    b.HasIndex("SectionId");

                    b.ToTable("SectionCapacity");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SubjectTitle");

                    b.HasKey("SubjectId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.TEnrolledSubject", b =>
                {
                    b.Property<long>("TEnrolledSubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId");

                    b.Property<int>("SectionId");

                    b.Property<int>("SubjectId");

                    b.Property<long>("TeacherId");

                    b.Property<long>("Year");

                    b.HasKey("TEnrolledSubjectId");

                    b.HasIndex("ClassId");

                    b.HasIndex("SectionId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TEnrolledSubject");
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.GuardianInfo", b =>
                {
                    b.Property<long>("GuardianInfoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GDesignationF");

                    b.Property<string>("GDesignationM");

                    b.Property<string>("GEmailF");

                    b.Property<string>("GEmailM");

                    b.Property<string>("GNameF");

                    b.Property<string>("GNameM");

                    b.Property<string>("GOccupationF");

                    b.Property<string>("GOccupationM");

                    b.Property<string>("GOrganisationF");

                    b.Property<string>("GOrganisationM");

                    b.Property<long>("GPhoneF");

                    b.Property<long>("GPhoneM");

                    b.Property<long>("StudentId");

                    b.HasKey("GuardianInfoId");

                    b.HasIndex("StudentId");

                    b.ToTable("GuardianInfo");
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.Student", b =>
                {
                    b.Property<long>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId");

                    b.Property<DateTime>("DateOfAdmission");

                    b.Property<int>("SectionId");

                    b.Property<string>("StudentBG");

                    b.Property<string>("StudentCAddress");

                    b.Property<DateTime>("StudentDOB");

                    b.Property<string>("StudentGender");

                    b.Property<string>("StudentName");

                    b.Property<string>("StudentNationality");

                    b.Property<string>("StudentPAddress");

                    b.Property<string>("StudentPhoto");

                    b.Property<long>("Year");

                    b.HasKey("StudentId");

                    b.HasIndex("ClassId");

                    b.HasIndex("SectionId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.Teacher", b =>
                {
                    b.Property<long>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TeacherBG");

                    b.Property<string>("TeacherCAddress");

                    b.Property<DateTime>("TeacherDOB");

                    b.Property<string>("TeacherDesignation");

                    b.Property<string>("TeacherEmail");

                    b.Property<string>("TeacherGender");

                    b.Property<DateTime>("TeacherJD");

                    b.Property<string>("TeacherName");

                    b.Property<string>("TeacherNationality");

                    b.Property<string>("TeacherPAddress");

                    b.Property<long>("TeacherPhoneNo");

                    b.Property<string>("TeacherPhoto");

                    b.HasKey("TeacherId");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.TeacherDocSubmitted", b =>
                {
                    b.Property<long>("TeacherDocSubmittedId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HSCCertificate");

                    b.Property<bool>("HSCMarksheet");

                    b.Property<bool>("HonsCertificate");

                    b.Property<bool>("HonsMarksheet");

                    b.Property<bool>("SSCCertificate");

                    b.Property<bool>("SSCMarksheet");

                    b.Property<long>("TeacherId");

                    b.HasKey("TeacherDocSubmittedId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherDocSubmitted");
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.TeacherQualification", b =>
                {
                    b.Property<long>("TeacherQualificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("HSCGrade");

                    b.Property<string>("HSCInstitute");

                    b.Property<long>("HSCYear");

                    b.Property<double>("HonsGrade");

                    b.Property<string>("HonsInstitute");

                    b.Property<long>("HonsYear");

                    b.Property<double>("SSCGrade");

                    b.Property<string>("SSCInstitute");

                    b.Property<long>("SSCYear");

                    b.Property<long>("TeacherId");

                    b.HasKey("TeacherQualificationId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherQualification");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Teachers.Models.MarkSubmission", b =>
                {
                    b.Property<long>("MarkSubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassTest");

                    b.Property<int>("ClassTestPercentage");

                    b.Property<DateTime>("Date");

                    b.Property<int>("ExamTypeId");

                    b.Property<int>("MarkTerm");

                    b.Property<long>("StudentId");

                    b.Property<int>("SubjectId");

                    b.Property<long>("TeacherId");

                    b.HasKey("MarkSubmissionId");

                    b.HasIndex("ExamTypeId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("MarkSubmission");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Teachers.Models.StudentAttendance", b =>
                {
                    b.Property<long>("StudentAttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Remark");

                    b.Property<int>("Status");

                    b.Property<long>("StudentId");

                    b.Property<int>("SubjectId");

                    b.HasKey("StudentAttendanceId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentAttendance");
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.SectionCapacity", b =>
                {
                    b.HasOne("SchoolManagement.Areas.Admin.Models.Class", "Class")
                        .WithMany("SectionCapacities")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.Admin.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolManagement.Areas.Admin.Models.TEnrolledSubject", b =>
                {
                    b.HasOne("SchoolManagement.Areas.Admin.Models.Class", "Class")
                        .WithMany("TEnrolledSubjects")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.Admin.Models.Section", "Section")
                        .WithMany("TEnrolledSubjects")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.Admin.Models.Subject", "Subject")
                        .WithMany("TEnrolledSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.AdmissionOfficer.Models.Teacher", "Teacher")
                        .WithMany("TEnrolledSubjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.GuardianInfo", b =>
                {
                    b.HasOne("SchoolManagement.Areas.AdmissionOfficer.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.Student", b =>
                {
                    b.HasOne("SchoolManagement.Areas.Admin.Models.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.Admin.Models.Section", "Section")
                        .WithMany("Students")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.TeacherDocSubmitted", b =>
                {
                    b.HasOne("SchoolManagement.Areas.AdmissionOfficer.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolManagement.Areas.AdmissionOfficer.Models.TeacherQualification", b =>
                {
                    b.HasOne("SchoolManagement.Areas.AdmissionOfficer.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolManagement.Areas.Teachers.Models.MarkSubmission", b =>
                {
                    b.HasOne("SchoolManagement.Areas.Admin.Models.ExamType", "ExamType")
                        .WithMany("MarkSubmissions")
                        .HasForeignKey("ExamTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.AdmissionOfficer.Models.Student", "Student")
                        .WithMany("MarkSubmissions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.Admin.Models.Subject", "Subject")
                        .WithMany("MarkSubmissions")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.AdmissionOfficer.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolManagement.Areas.Teachers.Models.StudentAttendance", b =>
                {
                    b.HasOne("SchoolManagement.Areas.AdmissionOfficer.Models.Student", "Student")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolManagement.Areas.Admin.Models.Subject", "Subject")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
