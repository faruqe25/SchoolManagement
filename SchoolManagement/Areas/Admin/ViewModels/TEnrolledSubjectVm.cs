using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.ViewModels
{
    public class TEnrolledSubjectVm
    {
        public Int64 tcrsbjctserialnoVM { get; set; }
        [Required]
        [Display(Name ="Teacher Name")]
        public Int64 tcridVM { get; set; }
        [Required]
        [Display(Name = "Subject Name")]
        public int sbjctidVM { get; set; }
        [Required]
        [Display(Name = "Class Name")]
        public int clsidVM { get; set; }
        [Required]
        [Display(Name = "Section Name")]
        public int secidVM { get; set; }
        public Int64 tcrsbjctyearVM { get; set; }
    }
}
