using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.ViewModels
{
    public class SectionVm
    {
        public int Sl { get; set; }
        public int SectionId { get; set; }
        [Required]
        [Display(Name = "Section Name")]
        public string SectionName { get; set; }
    }
}
