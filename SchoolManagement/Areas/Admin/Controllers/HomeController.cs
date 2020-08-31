using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.Admin.ViewModels;
using SchoolManagement.Database;
using X.PagedList;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClass(ClassVm c)
        {
            if (ModelState.IsValid)
            {
                var i = _context.Class.AsNoTracking().Where(s => s.ClassName == c.ClassName).FirstOrDefault();
                if (i == null)
                {
                    Class cs = new Class()
                    {
                        ClassId = c.ClassId,
                        ClassName = c.ClassName
                    };
                    _context.Class.Add(cs);
                    _context.SaveChanges();
                    return RedirectToAction("ClassList");
                }
                else
                {
                    ViewBag.SMS = "You Have Already this Class";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult ClassList(int  Page=1)
        {
            var r = _context.Class.ToList();
            
            if (r.Count == 0)
            {
                return RedirectToAction("Error");
            }
            List<ClassVm> rl = new List<ClassVm>();
            int c = 1;
            foreach (var item in r)
            {
                ClassVm rlvm = new ClassVm()
                {   Sl = c,
                    ClassId = item.ClassId,
                    ClassName = item.ClassName
                };
                rl.Add(rlvm);
                c++;
            }
           var list = rl.ToPagedList(Page,5);///Converting The ClassList into XPageList
            return View(list);
        }

        public IActionResult UpdateClass(int id)
        {
            
            Class i = _context.Class.AsNoTracking().Where(q => q.ClassId == id).FirstOrDefault();
            if(i==null)
            {
                return RedirectToAction("Error");
            }
            ClassVm l = new ClassVm()
            {
                ClassId = i.ClassId,
                ClassName = i.ClassName
            };
            return View(l);
        }
        [HttpPost]
        public IActionResult UpdateClass(ClassVm u)
        {
            if (ModelState.IsValid)
            {
                    Class l = new Class()
                    {
                        ClassId = u.ClassId,
                        ClassName = u.ClassName
                    };
                    _context.Class.Update(l);
                    _context.SaveChanges();
                    return RedirectToAction("ClassList");
            }
            else
            {
                return View();
            }
        }

        public IActionResult AddSection()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSection(SectionVm c)
        {

            if (ModelState.IsValid)
            {
                var i = _context.Section.AsNoTracking().Where(s => s.SectionName == c.SectionName).FirstOrDefault();
                if (i == null)
                {
                    Section cs = new Section()
                    {
                        SectionId = c.SectionId,
                        SectionName = c.SectionName
                    };
                    _context.Section.Add(cs);
                    _context.SaveChanges();
                    return RedirectToAction("SectionList");
                }
                else
                {
                    ViewBag.SMS = "You Have Already this Section";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult SectionList(int Page = 1)
        {
            var r = _context.Section.ToList();

            if (r.Count == 0)
            {
                return RedirectToAction("Error");
            }
            List<SectionVm> rl = new List<SectionVm>();
            int c = 1;
            foreach (var item in r)
            {
                SectionVm rlvm = new SectionVm()
                {
                    Sl = c,
                    SectionId = item.SectionId,
                    SectionName = item.SectionName
                };
                rl.Add(rlvm);
                c++;
            }
            var list = rl.ToPagedList(Page, 5);///Converting The ClassList into XPageList
            return View(list);
        }
        public IActionResult UpdateSection(int id)
        {

            Section i = _context.Section.AsNoTracking().Where(q => q.SectionId == id).FirstOrDefault();
            if (i == null)
            {
                return RedirectToAction("Error");
            }
            SectionVm l = new SectionVm()
            {
                SectionId = i.SectionId,
                SectionName = i.SectionName
            };
            return View(l);
        }
        [HttpPost]
        public IActionResult UpdateSection(SectionVm u)
        {
            if (ModelState.IsValid)
            {
                Section l = new Section()
                {
                    SectionId = u.SectionId,
                    SectionName = u.SectionName
                };
                _context.Section.Update(l);
                _context.SaveChanges();
                return RedirectToAction("SectionList");
            }
            else
            {
                return View();
            }
        }

        public IActionResult SectionCapacity()
        {
            //Get the List of Class
            var i = _context.Class.ToList();
            i.Insert(0, new Class { ClassId = 0, ClassName = "Select" });
            ViewBag.Class = new SelectList(i, "ClassId", "ClassName");
            //Get the List of Section
            var j = _context.Section.ToList();
            j.Insert(0, new Section { SectionId = 0, SectionName = "Select" });
            ViewBag.Section = new SelectList(j, "SectionId", "SectionName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SectionCapacity(SectionCapacityVm sb)
        {

            if (ModelState.IsValid && sb.ClassId!=0 && sb.SectionId!=0 && sb.Capacity!=0)
            {

                var sec = await  _context.SectionCapacity.AsNoTracking().Where(st => st.ClassId == sb.ClassId && st.SectionId == sb.SectionId).FirstOrDefaultAsync();

                if (sec == null)
                {
                    SectionCapacity s = new SectionCapacity()
                    {
                        ClassId = sb.ClassId,
                        Capacity = sb.Capacity,
                        SectionId = sb.SectionId,
                        SectionCapacityId = sb.SectionCapacityId,
                        SectionYear = DateTime.Now.Year
                    };
                    await _context.SectionCapacity.AddAsync(s);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("SectionCapacityList");
                }
                else
                {
                    var i = _context.Class.ToList();
                    i.Insert(0, new Class { ClassId = 0, ClassName = "Select" });
                    ViewBag.Class = new SelectList(i, "ClassId", "ClassName");
                    //Get the List of Section
                    var j = _context.Section.ToList();
                    j.Insert(0, new Section { SectionId = 0, SectionName = "Select" });
                    ViewBag.Section = new SelectList(j, "SectionId", "SectionName");

                    ViewBag.SMS = "OPS !!! You have already registered the section for this class";
                    return View();
                }

            }
            else
            {
                var i = _context.Class.ToList();
                i.Insert(0, new Class { ClassId = 0, ClassName = "Select" });
                ViewBag.Class = new SelectList(i, "ClassId", "ClassName");
                //Get the List of Section
                var j = _context.Section.ToList();
                j.Insert(0, new Section { SectionId = 0, SectionName = "Select" });
                ViewBag.Section = new SelectList(j, "SectionId", "SectionName");

                return View();

            }
            
        }
        public async Task<IActionResult> SectionCapacityList(int Page=1)
        {
            List<Class> c =await _context.Class.AsNoTracking().ToListAsync();
            List<Section> s = await _context.Section.AsNoTracking().ToListAsync();
            List<SectionCapacity> st = await _context.SectionCapacity.AsNoTracking().ToListAsync();
            var query = from cs in c
                        join se in st on cs.ClassId equals se.ClassId
                        join sb in s on se.SectionId equals sb.SectionId
                        select new
                        {
                            primary = se.SectionCapacityId,
                            ClassName = cs.ClassName,
                            SectionName = sb.SectionName,
                            SectionCapacity = se.Capacity,
                            Year = se.SectionYear
                        };
            if (query.Count()==0)
            {
                return RedirectToAction("Error");
            }
            List<SectionCapacityVm> sevm = new List<SectionCapacityVm>();
            int serial = 1;
            foreach (var item in query)
            {
                SectionCapacityVm ss = new SectionCapacityVm()
                {
                    Sl = serial,
                    Capacity = item.SectionCapacity,
                    SectionName = item.SectionName,
                    SectionYear = item.Year,
                    ClassName = item.ClassName,
                    SectionCapacityId = item.primary

                };
                serial++;
                sevm.Add(ss);

            }

            var List =await sevm.ToPagedListAsync(Page,5);

            return View(List);

           
        }
        public async Task<IActionResult> UpdateSectionCapacity(int u)
        {

            //Get the List of Class
            var i =await _context.Class.ToListAsync();
            i.Insert(0, new Class { ClassId = 0, ClassName = "Select" });
            ViewBag.Class = new SelectList(i, "ClassId", "ClassName");
            //Get the List of Section
            var j =await _context.Section.ToListAsync();
            j.Insert(0, new Section { SectionId = 0, SectionName = "Select" });
            ViewBag.Section = new SelectList(j, "SectionId", "SectionName");
            var sec =await  _context.SectionCapacity.Where(st => st.SectionCapacityId == u).FirstOrDefaultAsync();
            if(sec==null)
            {
                return RedirectToAction("Error");
            }
            SectionCapacityVm s = new SectionCapacityVm()
            {
                Capacity = sec.Capacity,
                SectionId = sec.SectionId,
                SectionYear = sec.SectionYear,
                ClassId = sec.ClassId,
                SectionCapacityId = sec.SectionCapacityId
            };
            return View(s);


        }
        [HttpPost]
        public async Task<IActionResult> UpdateSectionCapacity(SectionCapacityVm sb)
        { 
            if (ModelState.IsValid && sb.ClassId != 0 && sb.SectionId != 0)
            {
                SectionCapacity s = new SectionCapacity()
                {
                    ClassId = sb.ClassId,
                    Capacity = sb.Capacity,
                    SectionId = sb.SectionId,
                    SectionCapacityId = sb.SectionCapacityId,
                    SectionYear = DateTime.Now.Year
                };
                 _context.SectionCapacity.Update(s);
                await _context.SaveChangesAsync();
                return RedirectToAction("SectionCapacityList");
            }
            else
            {
                //Get the List of Class
                var i = _context.Class.ToList();
                i.Insert(0, new Class { ClassId = 0, ClassName = "Select" });
                ViewBag.Class = new SelectList(i, "ClassId", "ClassName");
                //Get the List of Section
                var j = _context.Section.ToList();
                j.Insert(0, new Section { SectionId = 0, SectionName = "Select" });
                ViewBag.Section = new SelectList(j, "SectionId", "SectionName");
                return View();
            }

        }
        public IActionResult AddSubject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectVm sb)
        {
            if (ModelState.IsValid)
            {
                Subject s = new Subject()
                {
                    SubjectId = sb.SubjectId,
                    SubjectTitle = sb.SubjectTitle
                };
                await _context.Subject.AddAsync(s);
                await _context.SaveChangesAsync();
                ModelState.Clear();
                return RedirectToAction("SubjectList");
            }
            else
            {
                return View();
            }
        }
        public async Task< IActionResult> SubjectList(int Page=1)
        {
            var s =await _context.Subject.ToListAsync();
            if(s.Count==0)
            {
                return RedirectToAction("Error");
            }
            
            List<SubjectVm> sv = new List<SubjectVm>();
            int c = 1;
            foreach (var item in s)
            {
                SubjectVm vm = new SubjectVm()
                {
                    Sl=c,
                    SubjectId = item.SubjectId,
                    SubjectTitle = item.SubjectTitle
                };
                sv.Add(vm);
                c++;
            }
            var List =await sv.ToPagedListAsync(Page,5);
            return View(List);
        }
        public async Task<IActionResult> UpdateSubject(int id)
        {
            Subject s = await _context.Subject.AsNoTracking().Where(sb => sb.SubjectId == id).FirstOrDefaultAsync();
            if(s==null)
            {
                return RedirectToAction("Error");
            }
            SubjectVm vm = new SubjectVm()
            {
                SubjectId = s.SubjectId,
                SubjectTitle = s.SubjectTitle
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSubject(SubjectVm sb)
        {
            if(ModelState.IsValid)
            {
                Subject s = new Subject()
                {
                    SubjectId = sb.SubjectId,
                    SubjectTitle = sb.SubjectTitle
                };
                _context.Subject.Update(s);
                await _context.SaveChangesAsync();
                return RedirectToAction("SubjectList");
            }
            return View();
        }

        public JsonResult VerifyClassForSection(int id)
        {
            var d = _context.SectionCapacity.AsNoTracking().Where(s => s.ClassId == id).ToList();
            var e = _context.Section.AsNoTracking().ToList();
            var query = from a in d
                        join b in e on a.SectionId equals b.SectionId
                        select new
                        {
                            SectionId=b.SectionId,
                            SectionName=b.SectionName
                        };


            var slist = new SelectList(query, "SectionId", "SectionName");

            return Json(slist);
        }


        public IActionResult SubjectAssignToTeacher()
        {
            //for the class
            var Subject = _context.Subject.AsNoTracking().ToList();
            ViewBag.Subject = new SelectList(Subject, "SubjectId", "SubjectTitle");
            //for the class
            var Class = _context.Class.AsNoTracking().ToList();
            ViewBag.Class = new SelectList(Class, "ClassId", "ClassName");
            //for thr teacher
            var Teacher = _context.Teacher.AsNoTracking().ToList();
            ViewBag.Teacher = new SelectList(Teacher, "TeacherId", "TeacherName");



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubjectAssignToTeacher(TEnrolledSubjectVm tb)
        {
            //for the class
            var Subject = _context.Subject.AsNoTracking().ToList();
            ViewBag.Subject = new SelectList(Subject, "SubjectId", "SubjectTitle");
            //for the class
            var Class = _context.Class.AsNoTracking().ToList();
            ViewBag.Class = new SelectList(Class, "ClassId", "ClassName");
            //for thr teacher
            var Teacher = _context.Teacher.AsNoTracking().ToList();
            ViewBag.Teacher = new SelectList(Teacher, "TeacherId", "TeacherName");
            var check = await _context.TEnrolledSubject.AsNoTracking().Where(s => s.ClassId == tb.clsidVM && s.SectionId == tb.secidVM && s.SubjectId == tb.sbjctidVM).FirstOrDefaultAsync();
            if (check != null)
            {
                ViewBag.MSG = "Already assigned";
                return View();
            }
            if (ModelState.IsValid)
            {
                TEnrolledSubject enrolledSubject = new TEnrolledSubject()
                {
                    ClassId = tb.clsidVM,
                    SectionId = tb.secidVM,
                    Year = DateTime.Now.Year,
                    TEnrolledSubjectId = 0,
                    SubjectId = tb.sbjctidVM,
                    TeacherId = tb.tcridVM

                };
                _context.TEnrolledSubject.Add(enrolledSubject);
                await _context.SaveChangesAsync();
               

                return RedirectToAction("ListAssignedSubject");
            }
            return View();
            
        }

        public async Task<IActionResult> ListAssignedSubject(int Page=1)
        {
            var section =await _context.Section.AsNoTracking().ToListAsync();
            var enrolledSubjects = await _context.TEnrolledSubject.AsNoTracking().ToListAsync();
            var cls = await _context.Class.AsNoTracking().ToListAsync();
            var sub = await _context.Subject.AsNoTracking().ToListAsync();
            var teacher = await _context.Teacher.AsNoTracking().ToListAsync();
            if(section.Count==0 || enrolledSubjects.Count == 0
                || cls.Count == 0 || sub.Count == 0 ||
                teacher.Count == 0 )
            {
                return RedirectToAction("Error");
            }
            var query = from sec in section

                        join en in enrolledSubjects on sec.SectionId equals en.SectionId
                        join su in sub on en.SubjectId equals su.SubjectId
                        join te in teacher on en.TeacherId equals te.TeacherId
                        join cl in cls on en.ClassId equals cl.ClassId
                        select new
                        {
                            Name = te.TeacherName,
                            Class = cl.ClassName,
                            Section = sec.SectionName,
                            Subject = su.SubjectTitle,
                            primaykey = en.TEnrolledSubjectId

                        };

            if (query.Count() == 0) { return RedirectToAction("Error"); }
            List<TeacherAssignedSubList> tc = new List<TeacherAssignedSubList>();
            int sl = 1;
            foreach (var item in query)
            {
                TeacherAssignedSubList t = new TeacherAssignedSubList()
                {
                    Class = item.Class,
                    Name = item.Name,
                    Section = item.Section,
                    Subject = item.Subject,
                    Pk = item.primaykey


                };
                t.Sl = sl;
                tc.Add(t);
                sl++;

            }

            var List =await  tc.ToPagedListAsync(Page, 5);
            return View(List);
        }

        public async Task<IActionResult> UpdateAssignedSubject(int id)

        {
            //for the class
            var Subject = _context.Subject.AsNoTracking().ToList();
            ViewBag.Subject = new SelectList(Subject, "SubjectId", "SubjectTitle");
            //for the class
            var Class = _context.Class.AsNoTracking().ToList();
            ViewBag.Class = new SelectList(Class, "ClassId", "ClassName");
            //for thr teacher
            var Teacher = _context.Teacher.AsNoTracking().ToList();
            ViewBag.Teacher = new SelectList(Teacher, "TeacherId", "TeacherName");
            //GEt the specific data
            TEnrolledSubject data = await _context.TEnrolledSubject.AsNoTracking().Where(s => s.TEnrolledSubjectId == id).FirstOrDefaultAsync();
            if (data==null) {
                return RedirectToAction("Error");
            }
            TEnrolledSubjectVm tdata = new TEnrolledSubjectVm()
            {
                tcrsbjctserialnoVM = data.TEnrolledSubjectId,
                clsidVM = data.ClassId,
                secidVM = data.SectionId,
                sbjctidVM = data.SubjectId,
                tcridVM = data.TeacherId,
                tcrsbjctyearVM = data.Year
            };
            var d = _context.SectionCapacity.AsNoTracking().Where(s => s.ClassId == tdata.clsidVM).ToList();
            var e = _context.Section.AsNoTracking().ToList();
            var query = from a in d
                        join b in e on a.SectionId equals b.SectionId
                        select new
                        {
                            SectionId = b.SectionId,
                            SectionName = b.SectionName
                        };


            var slist = new SelectList(query, "SectionId", "SectionName");
            ViewBag.Section = slist;


            return View(tdata);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAssignedSubject(TEnrolledSubjectVm td)
        {
            //for the class
            var Subject = _context.Subject.AsNoTracking().ToList();
            ViewBag.Subject = new SelectList(Subject, "SubjectId", "SubjectTitle");
            //for the class
            var Class = _context.Class.AsNoTracking().ToList();
            ViewBag.Class = new SelectList(Class, "ClassId", "ClassName");
            //for thr teacher
            var Teacher = _context.Teacher.AsNoTracking().ToList();
            ViewBag.Teacher = new SelectList(Teacher, "TeacherId", "TeacherName");
            //var check = await _context.TEnrolledSubject.AsNoTracking().Where(s => s.ClassId == td.clsidVM && s.SectionId == td.secidVM && s.SubjectId == td.sbjctidVM).FirstOrDefaultAsync();
            //if (check != null)
            //{
            //    ViewBag.MSG = "Already assigned";
            //    return View();
            //}
            TEnrolledSubject enrolledSubject = new TEnrolledSubject()
            {
                ClassId = td.clsidVM,
                SectionId = td.secidVM,
                Year = td.tcrsbjctyearVM,
                TEnrolledSubjectId = td.tcrsbjctserialnoVM,
                SubjectId = td.sbjctidVM,
                TeacherId = td.tcridVM

            };
            _context.TEnrolledSubject.Update(enrolledSubject);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListAssignedSubject");
        }

        public IActionResult AddExamType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddExamType(ExamTypeVm ex)
        {
            if (ModelState.IsValid)
            {
                ExamType e = new ExamType()
                {
                    ExamTypeId = 0,
                    ExamName = ex.exmnameVM
                };
                await _context.ExamType.AddAsync(e);
                await _context.SaveChangesAsync();
                return RedirectToAction("ExamTypeList");
            }
           
            return View();
        }
        public async Task<IActionResult> ExamTypeList()
        {
            int count = 1;
            var Ex = await _context.ExamType.AsNoTracking().ToListAsync();
            if (Ex.Count() == 0) { return RedirectToAction("Error"); }
            List<ExamTypeVm> e = new List<ExamTypeVm>();
            foreach (var item in Ex)
            {
                ExamTypeVm ext = new ExamTypeVm()
                {
                    Serial = count,
                    exmnameVM = item.ExamName,
                    exmidVM = item.ExamTypeId

                };
                e.Add(ext);
                count++;

            }
            return View(e);
        }
        public IActionResult UpdateExam(int id)
        {
            var e = _context.ExamType.AsNoTracking().Where(s => s.ExamTypeId == id).FirstOrDefault();
            if (e == null) { return RedirectToAction("Error"); }
            ExamTypeVm ext = new ExamTypeVm()
            {

                exmnameVM = e.ExamName,
                exmidVM = e.ExamTypeId

            };
            return View(ext);
        }
        [HttpPost]
        public IActionResult UpdateExam(ExamTypeVm ex)
        {
            ExamType ext = new ExamType()
            {
                ExamTypeId = ex.exmidVM,
                ExamName = ex.exmnameVM
            };
            _context.ExamType.Update(ext);
            _context.SaveChanges();

            return RedirectToAction("ExamTypeList");
        }
        public IActionResult Error()
        {
            return View();
        }


    }
}