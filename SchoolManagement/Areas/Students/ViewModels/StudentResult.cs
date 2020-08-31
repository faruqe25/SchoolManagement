using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Students.ViewModels
{
    public class StudentResult
    {
        public int ExamType { get; set; }
        public string ExamTypeName { get; set; }
        public string StudentName { get; set; }
        public string StudentClass { get; set; }
        public string Section { get; set; }
        public string SubjectName { get; set; }
        public int Mark { get; set; }
        public int ClassTest { get; set; }

    }
}
