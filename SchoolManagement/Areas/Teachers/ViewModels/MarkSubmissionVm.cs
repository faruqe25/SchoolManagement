using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Teachers.ViewModels
{
    public class MarkSubmissionVm
    {

        [Required]
        [Display(Name = "Exam Mark")]
        public int MarkTerm { get; set; }
        [Required]
        [Display(Name = "Class Test")]
        public int ClassTest { get; set; }
        [Required]
        [Display(Name = "Percentage")]
        public int ClassTestPercentage { get; set; }
        public int ExamType { get; set; }
        public Int64 StudentId { get; set; }
        public String StudentName { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public Int64 TeacherId { get; set; }
        public int SecionId { get; set; }
        ///ForList Mark
        public Int64 Pk { get; set; }
        public int Sl { get; set; }
        public string Sec { get; set; }
        public string Sub { get; set; }
        public string Term { get; set; }
        public string Class { get; set; }
        //
        public DateTime Date { get; set; }


    }
}
