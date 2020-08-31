using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.Teachers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.AdmissionOfficer.Models
{
    public class Teacher
    {
        public Teacher()
        {
            TEnrolledSubjects = new HashSet<TEnrolledSubject>();
            MarkSubmissions = new HashSet<MarkSubmission>();
        }
        [Key]
        public Int64 TeacherId { get; set; }
        public string TeacherName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TeacherDOB { get; set; }
        public string TeacherBG { get; set; }
        public DateTime TeacherJD { get; set; }
        public string TeacherGender { get; set; }
        public string TeacherNationality { get; set; }
        public string TeacherPAddress { get; set; }
        public string TeacherCAddress { get; set; }
        public Int64 TeacherPhoneNo { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherPhoto { get; set; }
        public string TeacherDesignation { get; set; }
        public virtual ICollection<TEnrolledSubject> TEnrolledSubjects { get; set; }
        public virtual ICollection<MarkSubmission> MarkSubmissions { get; set; }
    }
}
