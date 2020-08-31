using SchoolManagement.Areas.AdmissionOfficer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Models
{
    public class Class
    {
        public Class()
        {
            TEnrolledSubjects = new HashSet<TEnrolledSubject>();
            SectionCapacities = new HashSet<SectionCapacity>();
            Students = new HashSet<Student>();
        }
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public virtual ICollection<TEnrolledSubject> TEnrolledSubjects { get; set; }
        public virtual ICollection<SectionCapacity> SectionCapacities { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
