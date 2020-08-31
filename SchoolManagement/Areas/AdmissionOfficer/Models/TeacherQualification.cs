using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.AdmissionOfficer.Models
{
    public class TeacherQualification
    {
        [Key]
        public Int64 TeacherQualificationId { get; set; }
       
        public string SSCInstitute { get; set; }
        public Int64 SSCYear { get; set; }
        public double SSCGrade { get; set; }
        public string HSCInstitute { get; set; }
        public Int64 HSCYear { get; set; }
        public double HSCGrade { get; set; }
        public string HonsInstitute { get; set; }
        public Int64 HonsYear { get; set; }
        public double HonsGrade { get; set; }
        public Int64 TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
