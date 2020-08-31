using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.ViewModels
{
    public class ClassVm
    {
        public int Sl { get; set; }
        public int ClassId { get; set;}
        [Required]
        [Display(Name="Class Name")]
        public string ClassName { get; set;}
    }
}
