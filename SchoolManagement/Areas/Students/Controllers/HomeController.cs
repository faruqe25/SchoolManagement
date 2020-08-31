using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Areas.Students.ViewModels;
using SchoolManagement.Database;

namespace SchoolManagement.Areas.Students.Controllers
{
    [Area("Students")]
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        
        public HomeController(DatabaseContext context)
        {
            _context = context;
          
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Result()
        {

            var query = from a in _context.MarkSubmission.Include(a => a.Student.Section)
                        .Include(x => x.Student.Class)
                        where a.StudentId == 1
                        group a by a.ExamTypeId into g

                        let temp = (
                        from val in g
                        select new
                        {
                            ExamType = val.ExamType,
                            Subject = val.Subject,
                            Mark = val.MarkTerm,
                            Student = val.Student,
                            ExamTypeId = val.ExamTypeId,
                            MarkSubmissionId = val.MarkSubmissionId,
                            StudentId = val.StudentId,
                            SubjectId = val.SubjectId,
                            SubjectName = val.Subject.SubjectTitle,
                            ClassTest=val.ClassTest


                        }
                        )
                        select temp;
            var ab = new List<StudentResult>();
            foreach (var item in query)
            {
                foreach (var i in item)
                {
                    var a = new StudentResult()
                    {
                        StudentName = i.Student.StudentName,
                        ExamType = i.ExamTypeId,
                        SubjectName = i.SubjectName,
                        StudentClass = i.Student.Class.ClassName,
                        Section = i.Student.Section.SectionName,
                        Mark = i.Mark,
                        ExamTypeName = i.ExamType.ExamName,
                        ClassTest=i.ClassTest
                    };
                    ab.Add(a);



                }
            }

           


            return View(ab);
        }
    }

}