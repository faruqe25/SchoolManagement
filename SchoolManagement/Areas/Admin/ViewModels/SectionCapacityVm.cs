using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.ViewModels
{
    public class SectionCapacityVm
    {
        public int Sl { get; set; }
        public int SectionCapacityId { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get; set; }
        [Required]
        [Display(Name="Section Capacity")]
        public int Capacity { get; set; }
        public Int64 SectionYear { get; set; }
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int SectionId { get; set; }
    }
}
