using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.ViewModels
{
    public class ExamTypeVm
    {
        public int Serial { get; set; }
        public int exmidVM { get; set; }
        [Required]
        [Display(Name ="Exam Name")]
        public string exmnameVM { get; set; }
    }
}
