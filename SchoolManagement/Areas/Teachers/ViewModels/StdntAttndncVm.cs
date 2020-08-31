using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Teachers.ViewModels
{
    public class StdntAttndncVm
    {
        public Int64 stdntattserialnoVM { get; set; }
        public Int64 stdntidVM { get; set; }
        [Required]
        [Display(Name ="Class Name")]
        public Int64 clsidVM { get; set; }
        public string secnameVM { get; set; }
        public DateTime stdntattdateVM { get; set; }
        public string stdntattremarkVM { get; set; }
        [Required]
        [Display(Name = "Subject Name")]
        public int sbjctidVM { get; set; }
        public double stdntattprcntVM { get; set; }
        [Required]
        [Display(Name = "Section Name")]
        public Int64 SecId { get; set; }
        /// for attendance
        public string Name { get; set; }
        public int Status { get; set; }
        public String Remark { get; set; }
        // Total Class
        public int Total { get; set; }
        public int Present { get; set; }

    }
}
