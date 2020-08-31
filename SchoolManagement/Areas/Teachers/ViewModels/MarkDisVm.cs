using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Teachers.ViewModels
{
    public class MarkDisVm
    { 
    public int Serial { get; set; }
    public Int64 Key { get; set; }
    public string MarkdistitleVM { get; set; }

    [Required]
    [Display(Name ="Class Name")]
    public int ClsIdVM { get; set; }
    [Required]
    [Display(Name = "Subject Name")]
    public int subsidVM { get; set; }
   
   
    public int First { get; set; }
    public int Mid { get; set; }
    public int Final { get; set; }
    public int Number { get; set; }
    public int TermTpe { get; set; }
    [Required]
    [Display(Name = "Section Name")]
    public int SectionId { get; set; }
}
}
