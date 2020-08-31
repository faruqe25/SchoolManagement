using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SchoolManagement.Areas.AdmissionOfficer.ViewModels
{
    public class StudentVm
    {
        public Int64 stdntidVM { get; set; }
        public Int64 Serial { get; set; }
        [Required]
        [Display(Name = "Student's Name")]
        public String stdntnameVM { get; set; }
        [Required]
        [Display(Name ="Class Name")]
        public int stdntclsVM { get; set; }
        public String stdntclsNameVM { get; set; }
        [Required]
        [Display(Name = "Student's Birthday")]
        public DateTime dobVM { get; set; }
        public int SectionId { get; set; } 


        [Display(Name = "Student's Gender")]
        [Required]
      
        public string stdntgenderVM { get; set; }
        [Required]
        [Display(Name = "Student's Nationality")]
        public string stdntnationVM { get; set; }
        [Display(Name = "Student's Permanent Address")]
        public String stdntpaddrssVM { get; set; }
        [Required]
        [Display(Name = "Student's Permanent Address")]
        public String stdntcaddrssVM { get; set; }

        [Display(Name = "Student's Photo")]
        public String stdntphotoVM { get; set; }
        public String stdntbgVM { get; set; }
        public DateTime stdntdoaVM { get; set; }
        [Display(Name = "Year")]
        
        public Int64 stdntyearVM { get; set; }


        ///Guardian Father

        public Int64 gserialnoFVM { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string gnameFVM { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public Int64 gphoneFVM { get; set; }
        public string gemailFVM { get; set; }
        public String gdesignationFVM { get; set; }
        public String gorganisationFVM { get; set; }
        [Required]
        [Display(Name = "Occupation")]
        public String goccupationFVM { get; set; }

        ///Guardian Mother

        public Int64 gserialnoMVM { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string gnameMVM { get; set; }
        public Int64 gphoneMVM { get; set; }
        public string gemailMVM { get; set; }
        public String gorganisationMVM { get; set; }
        public String gdesignationsMVM { get; set; }
        [Required]
        [Display(Name = "Occupation")]
        public String goccupationMVM { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (stdntclsVM ==0)
        //    {
        //        yield return new ValidationResult(
        //            $"Classic movies must have a release year earlier than.",
        //            new[] { "ReleaseDate" });
        //    }
        //}

        

    }
}
