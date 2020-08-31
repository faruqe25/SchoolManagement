using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "ExamType",
                columns: table => new
                {
                    ExamTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamType", x => x.ExamTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    LoginId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CurrentPassword = table.Column<string>(nullable: true),
                    FirstLoginStatus = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    ActiveStatus = table.Column<bool>(nullable: false),
                    DistinguishId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    SectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubjectTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeacherName = table.Column<string>(nullable: true),
                    TeacherDOB = table.Column<DateTime>(nullable: false),
                    TeacherBG = table.Column<string>(nullable: true),
                    TeacherJD = table.Column<DateTime>(nullable: false),
                    TeacherGender = table.Column<string>(nullable: true),
                    TeacherNationality = table.Column<string>(nullable: true),
                    TeacherPAddress = table.Column<string>(nullable: true),
                    TeacherCAddress = table.Column<string>(nullable: true),
                    TeacherPhoneNo = table.Column<long>(nullable: false),
                    TeacherEmail = table.Column<string>(nullable: true),
                    TeacherPhoto = table.Column<string>(nullable: true),
                    TeacherDesignation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "SectionCapacity",
                columns: table => new
                {
                    SectionCapacityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Capacity = table.Column<int>(nullable: false),
                    SectionYear = table.Column<long>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionCapacity", x => x.SectionCapacityId);
                    table.ForeignKey(
                        name: "FK_SectionCapacity_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionCapacity_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentName = table.Column<string>(nullable: true),
                    StudentBG = table.Column<string>(nullable: true),
                    StudentDOB = table.Column<DateTime>(nullable: false),
                    StudentGender = table.Column<string>(nullable: true),
                    StudentNationality = table.Column<string>(nullable: true),
                    StudentPAddress = table.Column<string>(nullable: true),
                    StudentCAddress = table.Column<string>(nullable: true),
                    DateOfAdmission = table.Column<DateTime>(nullable: false),
                    Year = table.Column<long>(nullable: false),
                    StudentPhoto = table.Column<string>(nullable: true),
                    ClassId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDocSubmitted",
                columns: table => new
                {
                    TeacherDocSubmittedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SSCMarksheet = table.Column<bool>(nullable: false),
                    SSCCertificate = table.Column<bool>(nullable: false),
                    HSCMarksheet = table.Column<bool>(nullable: false),
                    HSCCertificate = table.Column<bool>(nullable: false),
                    HonsMarksheet = table.Column<bool>(nullable: false),
                    HonsCertificate = table.Column<bool>(nullable: false),
                    TeacherId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDocSubmitted", x => x.TeacherDocSubmittedId);
                    table.ForeignKey(
                        name: "FK_TeacherDocSubmitted_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherQualification",
                columns: table => new
                {
                    TeacherQualificationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SSCInstitute = table.Column<string>(nullable: true),
                    SSCYear = table.Column<long>(nullable: false),
                    SSCGrade = table.Column<double>(nullable: false),
                    HSCInstitute = table.Column<string>(nullable: true),
                    HSCYear = table.Column<long>(nullable: false),
                    HSCGrade = table.Column<double>(nullable: false),
                    HonsInstitute = table.Column<string>(nullable: true),
                    HonsYear = table.Column<long>(nullable: false),
                    HonsGrade = table.Column<double>(nullable: false),
                    TeacherId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherQualification", x => x.TeacherQualificationId);
                    table.ForeignKey(
                        name: "FK_TeacherQualification_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEnrolledSubject",
                columns: table => new
                {
                    TEnrolledSubjectId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<long>(nullable: false),
                    TeacherId = table.Column<long>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEnrolledSubject", x => x.TEnrolledSubjectId);
                    table.ForeignKey(
                        name: "FK_TEnrolledSubject_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TEnrolledSubject_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TEnrolledSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TEnrolledSubject_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuardianInfo",
                columns: table => new
                {
                    GuardianInfoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GNameF = table.Column<string>(nullable: true),
                    GNameM = table.Column<string>(nullable: true),
                    GPhoneF = table.Column<long>(nullable: false),
                    GPhoneM = table.Column<long>(nullable: false),
                    GEmailF = table.Column<string>(nullable: true),
                    GEmailM = table.Column<string>(nullable: true),
                    GOccupationF = table.Column<string>(nullable: true),
                    GOccupationM = table.Column<string>(nullable: true),
                    GDesignationF = table.Column<string>(nullable: true),
                    GDesignationM = table.Column<string>(nullable: true),
                    GOrganisationM = table.Column<string>(nullable: true),
                    GOrganisationF = table.Column<string>(nullable: true),
                    StudentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuardianInfo", x => x.GuardianInfoId);
                    table.ForeignKey(
                        name: "FK_GuardianInfo_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarkSubmission",
                columns: table => new
                {
                    MarkSubmissionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MarkTerm = table.Column<int>(nullable: false),
                    ClassTest = table.Column<int>(nullable: false),
                    ClassTestPercentage = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ExamTypeId = table.Column<int>(nullable: false),
                    StudentId = table.Column<long>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkSubmission", x => x.MarkSubmissionId);
                    table.ForeignKey(
                        name: "FK_MarkSubmission_ExamType_ExamTypeId",
                        column: x => x.ExamTypeId,
                        principalTable: "ExamType",
                        principalColumn: "ExamTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarkSubmission_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarkSubmission_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarkSubmission_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendance",
                columns: table => new
                {
                    StudentAttendanceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    StudentId = table.Column<long>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendance", x => x.StudentAttendanceId);
                    table.ForeignKey(
                        name: "FK_StudentAttendance_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAttendance_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuardianInfo_StudentId",
                table: "GuardianInfo",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkSubmission_ExamTypeId",
                table: "MarkSubmission",
                column: "ExamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkSubmission_StudentId",
                table: "MarkSubmission",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkSubmission_SubjectId",
                table: "MarkSubmission",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkSubmission_TeacherId",
                table: "MarkSubmission",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionCapacity_ClassId",
                table: "SectionCapacity",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionCapacity_SectionId",
                table: "SectionCapacity",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                table: "Student",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SectionId",
                table: "Student",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_StudentId",
                table: "StudentAttendance",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_SubjectId",
                table: "StudentAttendance",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDocSubmitted_TeacherId",
                table: "TeacherDocSubmitted",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherQualification_TeacherId",
                table: "TeacherQualification",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TEnrolledSubject_ClassId",
                table: "TEnrolledSubject",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TEnrolledSubject_SectionId",
                table: "TEnrolledSubject",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TEnrolledSubject_SubjectId",
                table: "TEnrolledSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TEnrolledSubject_TeacherId",
                table: "TEnrolledSubject",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuardianInfo");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "MarkSubmission");

            migrationBuilder.DropTable(
                name: "SectionCapacity");

            migrationBuilder.DropTable(
                name: "StudentAttendance");

            migrationBuilder.DropTable(
                name: "TeacherDocSubmitted");

            migrationBuilder.DropTable(
                name: "TeacherQualification");

            migrationBuilder.DropTable(
                name: "TEnrolledSubject");

            migrationBuilder.DropTable(
                name: "ExamType");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Section");
        }
    }
}
