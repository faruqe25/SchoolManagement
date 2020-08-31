using Microsoft.EntityFrameworkCore;
using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.AdmissionOfficer.Models;
using SchoolManagement.Areas.Teachers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Database
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
        }
        
        public DbSet<Login> Login { get;set;}
        public DbSet<Class> Class { get;set;}
        public DbSet<Section> Section { get;set;}
        public DbSet<SectionCapacity> SectionCapacity { get;set;}
        public DbSet<Subject> Subject { get;set;}
        public DbSet<Teacher> Teacher { get;set;}
        public DbSet<TeacherQualification> TeacherQualification { get;set;}
        public DbSet<TeacherDocSubmitted> TeacherDocSubmitted { get;set;}
        public DbSet<Student> Student { get;set;}
        public DbSet<GuardianInfo> GuardianInfo { get;set;}
        public DbSet<TEnrolledSubject> TEnrolledSubject { get;set;}
        public DbSet<ExamType> ExamType { get;set;}
       
        public DbSet<MarkSubmission> MarkSubmission { get;set;}
        public DbSet<StudentAttendance> StudentAttendance { get; set; }






    }
}
