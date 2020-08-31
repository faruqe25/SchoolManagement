using SchoolManagement.Areas.AdmissionOfficer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Models
{
    public class Section
    {
        public Section()
        {
            TEnrolledSubjects = new HashSet<TEnrolledSubject>();
            Students = new HashSet<Student>();
        }
        [Key]
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public virtual ICollection<TEnrolledSubject> TEnrolledSubjects { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
