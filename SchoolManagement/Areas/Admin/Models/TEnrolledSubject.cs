using SchoolManagement.Areas.AdmissionOfficer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Models
{
    public class TEnrolledSubject
    {
        [Key]
        public Int64 TEnrolledSubjectId { get; set; }
        public Int64 Year { get; set; }
        public Int64 TeacherId { get; set; }

        public Teacher Teacher  { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
       
        public int ClassId { get; set; }
        public Class Class { get; set; }

    }
}
