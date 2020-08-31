using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.AdmissionOfficer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Teachers.Models
{
    public class StudentAttendance
    {
        [Key]
        public Int64 StudentAttendanceId { get; set; }
       
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
       
        public Int64 StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }


    }
}
