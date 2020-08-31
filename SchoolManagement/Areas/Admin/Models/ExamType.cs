using SchoolManagement.Areas.Teachers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Models
{
    public class ExamType
    {
        public ExamType()
        {
            MarkSubmissions = new HashSet<MarkSubmission>();

        }
        [Key]
        public int ExamTypeId { get; set; }
        public string ExamName { get; set; }
        public virtual ICollection<MarkSubmission> MarkSubmissions { get; set; }
    }
}
