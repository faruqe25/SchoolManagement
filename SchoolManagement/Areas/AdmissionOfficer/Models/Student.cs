using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.Teachers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.AdmissionOfficer.Models
{
    public class Student
    {
        public Student()
        {
            MarkSubmissions = new HashSet<MarkSubmission>();
            StudentAttendances = new HashSet<StudentAttendance>();
          
        }
        [Key]
        public Int64 StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentBG { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StudentDOB { get; set; }


        public string StudentGender { get; set; }
        public string StudentNationality { get; set; }
        public string StudentPAddress { get; set; }
        public string StudentCAddress { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public Int64 Year { get; set; }
        public string StudentPhoto { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public virtual ICollection<MarkSubmission>MarkSubmissions { get; set; }
        public virtual ICollection<StudentAttendance>StudentAttendances  { get; set; }
       
        
    }
}
