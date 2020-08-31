using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.AdmissionOfficer.ViewModels
{
    public class TeacherVm
    {
        public Int64 TcridVM { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string TcrnameVM { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public string TcrdesigVM { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        public DateTime TcrdobVM { get; set; }
        [Required]
        [Display(Name = "Blood Group")]
        public string TcrbgVM { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string TcrgenderVM { get; set; }
        public DateTime TcrjdVM { get; set; }
        [Required]
        [Display(Name = "Curent Address")]
        public string TcrnationVM { get; set; }
        public String TcrpaddrssVM { get; set; }
        [Required]
        [Display(Name = "Curent Address")]
        public String TcrcaddrssVM { get; set; }
        public String TcrphotoVM { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public Int64 TcrphoneVM { get; set; }
        public string TcremailVM { get; set; }

        ///Document Submission 
        public Int64 DocSLVM { get; set; }
        public Boolean TcrdocsubsscmarkVM { get; set; }
        public Boolean TcrdocsubssccrtfctVM { get; set; }
        public Boolean TcrdocsubhscmarkVM { get; set; }
        public Boolean TcrdocsubhsccrtfctVM { get; set; }
        public Boolean TcrdocsubhonsmarkVM { get; set; }
        public Boolean TcrdocsubhonscrtfctVM { get; set; }

        /// Teacher Qualification
        public Int64 QlSLVM { get; set; }
        [Required]
        [Display(Name ="SSC year")]
        public Int64 TcrqsscyearVM { get; set; }
        [Required]
        [Display(Name = "HSC year")]
        public Int64 TcrqhscyearVM { get; set; }
        [Required]
        [Display(Name = "Hons year")]
        public Int64 TcrqhonsyearVM { get; set; }
        [Required]
        [Display(Name = "SSC grade")]
        public double TcrqsscgradeVM { get; set; }
        [Required]
        [Display(Name = "HSC grade")]
        public double TcrqhscgradeVM { get; set; }
        [Required]
        [Display(Name = "Hons grade")]
        public double TcrqhonsgradeVM { get; set; }
        [Required]
        [Display(Name = "SSC Institude")]
        public String TcrqsscinsVM { get; set; }
        [Required]
        [Display(Name = "HSC Institude")]
        public String TcrqhscinsVM { get; set; }
        [Required]
        [Display(Name = "Hons Institude")]
        public String TcrqhonsinsVM { get; set; }
    }
}
