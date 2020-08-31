using SchoolManagement.Areas.Teachers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Models
{
    public class Subject
    {
        public Subject()
        {
            TEnrolledSubjects = new HashSet<TEnrolledSubject>();
            MarkSubmissions = new HashSet<MarkSubmission>();
            StudentAttendances = new HashSet<StudentAttendance>();
        }
        [Key]
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public virtual ICollection<TEnrolledSubject> TEnrolledSubjects { get; set; }
        public virtual ICollection<MarkSubmission> MarkSubmissions { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }

    }
}
