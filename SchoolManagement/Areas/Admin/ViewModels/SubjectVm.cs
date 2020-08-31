using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.ViewModels
{
    public class SubjectVm
    {
        public int Sl { get; set; }
        public int SubjectId { get; set; }
        [Required]
        [Display(Name="Subject Name")]
        public string SubjectTitle { get; set; }
    }
}
