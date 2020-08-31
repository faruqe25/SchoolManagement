using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.Teachers.Models;
using SchoolManagement.Areas.Teachers.ViewModels;
using SchoolManagement.Database;

namespace SchoolManagement.Areas.Teachers.Controllers
{    [Area("Teachers")]
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<JsonResult> VerifySection(int id)
        {
            var Teacherid = 1;
            var d =await  _context.SectionCapacity.AsNoTracking().Where(s => s.ClassId == id).ToListAsync();
            var e = await _context.Section.AsNoTracking().ToListAsync();
            var f = await _context.Class.AsNoTracking().ToListAsync();
            var st = await _context.TEnrolledSubject.AsNoTracking().Where(p => p.TeacherId == Teacherid).ToListAsync();
            var query = (from i in d
                        join j in st on i.ClassId equals j.ClassId
                        join k in e on j.SectionId equals k.SectionId
                        join l in f on j.ClassId equals l.ClassId
                       
                        select new
                        {
                            SectionSL = k.SectionId,
                            SectionName = k.SectionName
                        }).Distinct();


            var slist = new SelectList(query, "SectionSL", "SectionName");

            return Json(slist);
        }
        public async Task<JsonResult> VerifySubject(int id)
        {
            var Teacherid = 1;


            var sub = await _context.Subject.AsNoTracking().ToListAsync();
            var st = await _context.TEnrolledSubject.AsNoTracking().Where(p => p.TeacherId == Teacherid && p.SectionId==id).ToListAsync();

            var query = (from i in sub
                         join j in st on i.SubjectId equals j.SubjectId
                         select new
                         {
                             SubjectId = i.SubjectId,
                             SubjectName = i.SubjectTitle,
                            
                         }).Distinct();


            var slist = new SelectList(query, "SubjectId", "SubjectName");

            return Json(slist);
        }

        public IActionResult TakeAttendance()
        {
            var TeacherId = 1;
            var Attendance = _context.TEnrolledSubject.Where(se => se.TeacherId == TeacherId).ToList();
            List<Class> c = _context.Class.AsNoTracking().ToList();
           
            var query1 = (from cls in c
                         join at in Attendance on cls.ClassId equals at.ClassId 
                         select new
                         {
                             ClassId = cls.ClassId,
                             ClassName = cls.ClassName
                         }).Distinct();
            ViewBag.Class = new SelectList(query1, "ClassId", "ClassName");
           



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Attendance(StdntAttndncVm st)
        {
            var ClassName = await _context.Class.AsNoTracking().Where(s => s.ClassId == st.clsidVM).FirstOrDefaultAsync();
            ViewBag.ClassName = ClassName.ClassName;
            var SectionName = await _context.Section.AsNoTracking().Where(s=>s.SectionId == st.SecId).FirstOrDefaultAsync();
            ViewBag.SectionName = SectionName.SectionName;


            var stu = await _context.Student.AsNoTracking().Where(s => s.ClassId == st.clsidVM && s.SectionId == st.SecId).ToListAsync();
            var i = 1;

            var at = new List<StdntAttndncVm>();

            foreach (var item in stu)
            {
                StdntAttndncVm s = new StdntAttndncVm()
                {
                    Name = item.StudentName,
                    stdntidVM = item.StudentId,
                    stdntattremarkVM = "",
                    Status = 1,
                    stdntattserialnoVM = i,
                    SecId = item.SectionId,
                    sbjctidVM = st.sbjctidVM,
                    clsidVM = st.clsidVM

                };
                i++;
                at.Add(s);
            }


            return View(at);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmAttendance(List<StdntAttndncVm> lst)
        {
            var test = _context.StudentAttendance.AsNoTracking().
                Where(s => DateTime.Compare(s.Date,DateTime.Now.Date) == 0 &&
                s.Student.SectionId ==lst[0].SecId && s.SubjectId
                == lst[0].sbjctidVM && s.Student.ClassId== lst[0].clsidVM).FirstOrDefault();
            if(test!=null)
            {
                return RedirectToAction("Error");
            }

            for (int i = 0; i < lst.Count; i++)
            {
                StudentAttendance s = new StudentAttendance()
                {
                    StudentId = lst[i].stdntidVM,
                   
                    SubjectId = lst[i].sbjctidVM,
                    Remark = lst[i].Remark,
                    Status = lst[i].Status,
                    Date = DateTime.Now,


                };
                await _context.StudentAttendance.AddAsync(s);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("TakeAttendance");
        }


        public IActionResult AttendanceDetails()
        {

            var TeacherId = 1;
            var Attendance = _context.TEnrolledSubject.Where(se => se.TeacherId == TeacherId).ToList();
            List<Class> c = _context.Class.AsNoTracking().ToList();
          
            var query1 = (from cls in c
                         join at in Attendance on cls.ClassId equals at.ClassId
                         select new
                         {
                             ClassId = cls.ClassId,
                             ClassName = cls.ClassName
                         }).Distinct();
            ViewBag.Class = new SelectList(query1, "ClassId", "ClassName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AttendanceScript(StdntAttndncVm st)
        {


            var ClassName = await _context.Class.AsNoTracking().Where(s => s.ClassId == st.clsidVM).FirstOrDefaultAsync();
            ViewBag.ClassName = ClassName.ClassName;
            var SectionName = await _context.Section.AsNoTracking().Where(s =>  s.SectionId == st.SecId).FirstOrDefaultAsync();
            ViewBag.SectionName = SectionName.SectionName;
            var SubjectName = await _context.Subject.AsNoTracking().Where(s => s.SubjectId == st.sbjctidVM).FirstOrDefaultAsync();
            ViewBag.SubjectName = SubjectName.SubjectTitle;
            var attedence = await _context.StudentAttendance.AsNoTracking().Where(s => s.Student.ClassId == st.clsidVM && s.Student.SectionId == st.SecId && s.SubjectId == st.sbjctidVM).ToListAsync();

            var count = (from a in attedence
                         where a.SubjectId == st.sbjctidVM
                         group a by (a.StudentId) into s
                         select new
                         {
                             StudentId = s.Key,
                             Total = s.Distinct().Count(),
                             Present = s.Sum(a => a.Status)

                         }).ToList();

            var stu = _context.Student.AsNoTracking().Where(s => s.ClassId == st.clsidVM && s.SectionId == st.SecId).ToList();

            var query = from s in stu
                        join c in count on s.StudentId equals c.StudentId
                        select new
                        {
                            Name = s.StudentName,
                            ID = s.StudentId,
                            Total = c.Total,
                            Present = c.Present
                        };

            List<StdntAttndncVm> send = new List<StdntAttndncVm>();
            var cn = 1;
            foreach (var item in query)
            {
                StdntAttndncVm s = new StdntAttndncVm()
                {
                    Name = item.Name,
                    stdntidVM = item.ID,
                    Total = item.Total,
                    Present = item.Present,




                };
                s.stdntattserialnoVM = cn;
                cn++;
                send.Add(s);


            }
            return View(send);
        }
        public async Task<IActionResult> MarkSubmission()
        {

            var TeacherId = 1;
            var Attendance = _context.TEnrolledSubject.Where(se => se.TeacherId == TeacherId).ToList();
            List<Class> c = _context.Class.AsNoTracking().ToList();

            var query1 = (from cls in c
                          join at in Attendance on cls.ClassId equals at.ClassId
                          select new
                          {
                              ClassId = cls.ClassId,
                              ClassName = cls.ClassName
                          }).Distinct();
            ViewBag.Class = new SelectList(query1, "ClassId", "ClassName");

            ViewBag.Term = new SelectList(await _context.ExamType.AsNoTracking().ToListAsync(), "ExamTypeId", "ExamName");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SubmitMark(MarkDisVm mr)
        {
            var t = await _context.MarkSubmission.AsNoTracking().Where(s => s.Student.ClassId == mr.ClsIdVM && s.SubjectId == mr.subsidVM && s.Student.SectionId == mr.SectionId && s.ExamTypeId == mr.TermTpe).FirstOrDefaultAsync();

            if (t == null)
            {
                var Section = await _context.Section.AsNoTracking().Where(s => s.SectionId==mr.SectionId).FirstOrDefaultAsync();
                var Class = await _context.Class.AsNoTracking().Where(s => s.ClassId == mr.ClsIdVM).FirstOrDefaultAsync();
                var Student = await _context.Student.AsNoTracking().Where(s => s.ClassId == mr.ClsIdVM && s.SectionId == mr.SectionId).ToListAsync();
                var Subject = await _context.Subject.AsNoTracking().Where(s => s.SubjectId == mr.subsidVM).FirstOrDefaultAsync();
                var TermName = await _context.ExamType.AsNoTracking().Where(s => s.ExamTypeId == mr.TermTpe).FirstOrDefaultAsync();
                ViewBag.TermName = TermName.ExamName;
                ViewBag.Subject = Subject.SubjectTitle;
                ViewBag.Class = Class.ClassName;
                ViewBag.Section = Section.SectionName;

                List<MarkSubmissionVm> mrk = new List<MarkSubmissionVm>();
                int c = 1;
                foreach (var item in Student)
                {
                    MarkSubmissionVm m = new MarkSubmissionVm()
                    {
                        StudentId = item.StudentId,
                        StudentName = item.StudentName,
                        ClassId = item.ClassId,
                        SecionId = item.SectionId,
                    };
                    m.ExamType = TermName.ExamTypeId;
                    m.SubjectId = mr.subsidVM;
                    m.TeacherId = 1;
                    m.Sl = c;
                    mrk.Add(m);
                    c++;
                }

                return View(mrk);

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmSubmission(List<MarkSubmissionVm> mr)
        {
            for (int i = 0; i < mr.Count; i++)
            {
                MarkSubmission s = new MarkSubmission()
                {
                    StudentId = mr[i].StudentId,
                    ExamTypeId = mr[i].ExamType,
                    
                    MarkTerm = mr[i].MarkTerm,
                    SubjectId = mr[i].SubjectId,
                    ClassTest = mr[i].ClassTest,
                    Date = DateTime.Now,
                    TeacherId = 1

                };
                s.ClassTestPercentage = mr[0].ClassTestPercentage;
                await _context.MarkSubmission.AddAsync(s);
                await _context.SaveChangesAsync();

            }


            return RedirectToAction("MarkSubmission");
        }




        public IActionResult Error()
        {
            return View();
        }
    }
}