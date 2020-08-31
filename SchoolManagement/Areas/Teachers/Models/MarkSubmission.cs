using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.AdmissionOfficer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Teachers.Models
{
    public class MarkSubmission
    {
        [Key]
        public Int64 MarkSubmissionId { get; set; }
        public int MarkTerm { get; set; }
        public int ClassTest { get; set; }
        public int ClassTestPercentage { get; set; }
        public DateTime Date { get; set; }
        public int ExamTypeId { get; set; }
        public ExamType ExamType { get; set; }
        public Int64 StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Int64 TeacherId { get; set; }
        public Teacher Teacher { get; set; }

       
        
    }
}
