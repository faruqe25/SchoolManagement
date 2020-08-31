using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Areas.Admin.Models;
using SchoolManagement.Areas.AdmissionOfficer.Models;
using SchoolManagement.Areas.AdmissionOfficer.ViewModels;
using SchoolManagement.Database;
using SchoolManagement.Utility;
using X.PagedList;

namespace SchoolManagement.Areas.AdmissionOfficer.Controllers
{
    [Area("AdmissionOfficer")]
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IHostingEnvironment _imagepath;
        public HomeController(DatabaseContext context, IHostingEnvironment imagepath)
        {
            _context = context;
            _imagepath = imagepath;
        }
        public IActionResult AddTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherVm tr, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                Teacher tchr = new Teacher()
                {
                    TeacherId = 0,
                    TeacherName = tr.TcrnameVM,
                    TeacherBG = tr.TcrbgVM,
                    TeacherEmail = tr.TcremailVM,
                    TeacherCAddress = tr.TcrcaddrssVM,
                    TeacherPAddress = tr.TcrpaddrssVM,
                    TeacherDOB = tr.TcrdobVM,
                    TeacherGender = tr.TcrgenderVM,
                    TeacherNationality = tr.TcrnationVM,
                    TeacherPhoneNo = tr.TcrphoneVM,
                    TeacherJD = DateTime.Now.Date,
                    TeacherPhoto = tr.TcrphotoVM,
                    TeacherDesignation = tr.TcrdesigVM

                };
                string un = null;
                if (image != null)
                {
                    var path = Path.Combine(_imagepath.WebRootPath + "/images");
                    un = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filepath = Path.Combine(path, un);
                    using (var file = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(file);

                    }
                    
                    tchr.TeacherPhoto = un;
                }

                _context.Teacher.Add(tchr);
                await _context.SaveChangesAsync();
                ///Get the id of teacher 
                var TeacherId = tchr.TeacherId;
                //Teacher email
                var TeacherEmail = tchr.TeacherEmail;
                // Create random password 
                PasswordGenerator ps = new PasswordGenerator();
                //Create login
                Login lg = new Login()
                {
                    LoginId = 0,
                    Username = TeacherEmail,
                    Password = ps.RandomPassword(),
                    FirstLoginStatus = false,
                    ActiveStatus = true,
                    RoleId = 2,
                    DistinguishId = TeacherId

                };
                lg.CurrentPassword = lg.Password;
                _context.Login.Add(lg);
                await _context.SaveChangesAsync();

                ///Save the Document Information to the Database

                TeacherDocSubmitted doc = new TeacherDocSubmitted()
                {
                    TeacherId = TeacherId,
                    TeacherDocSubmittedId = 0,
                    SSCCertificate = tr.TcrdocsubhsccrtfctVM,
                    SSCMarksheet = tr.TcrdocsubsscmarkVM,
                    HSCCertificate = tr.TcrdocsubhsccrtfctVM,
                    HSCMarksheet = tr.TcrdocsubhscmarkVM,
                    HonsCertificate = tr.TcrdocsubhonscrtfctVM,
                    HonsMarksheet = tr.TcrdocsubhonsmarkVM
                };

                await _context.TeacherDocSubmitted.AddAsync(doc);
                await _context.SaveChangesAsync();

                TeacherQualification tq = new TeacherQualification()
                {
                    TeacherQualificationId = 0,
                    TeacherId = TeacherId,
                    SSCYear = tr.TcrqsscyearVM,
                    SSCGrade = tr.TcrqsscgradeVM,
                    SSCInstitute = tr.TcrqsscinsVM,
                    HSCYear = tr.TcrqhscyearVM,
                    HSCGrade = tr.TcrqhscgradeVM,
                    HSCInstitute = tr.TcrqhscinsVM,
                    HonsYear = tr.TcrqhonsyearVM,
                    HonsGrade = tr.TcrqhonsgradeVM,
                    HonsInstitute = tr.TcrqhonsinsVM
                };
                await _context.TeacherQualification.AddAsync(tq);
                await _context.SaveChangesAsync();



                ///Clear the model state

                ModelState.Clear();
                return View();
            }

            return View();
        }

        public async Task<IActionResult> TeacherList(int Page=1)

        {
            Int64 Count = 1;
            List<Teacher> tcr = await _context.Teacher.AsNoTracking().ToListAsync();
           
            if (tcr == null) { return RedirectToAction("Error"); }

            List<TeacherVm> tv = new List<TeacherVm>();

            foreach (var item in tcr)
            {

                TeacherVm t = new TeacherVm()
                {
                    TcridVM = item.TeacherId,
                    TcrnameVM = item.TeacherName,
                    TcrdesigVM = item.TeacherDesignation,
                    TcrdobVM = item.TeacherDOB,
                    TcrbgVM = item.TeacherBG,
                    TcrgenderVM = item.TeacherGender,
                    TcrjdVM = item.TeacherJD,
                    TcrnationVM = item.TeacherNationality,
                    TcrcaddrssVM = item.TeacherCAddress,
                    TcrpaddrssVM = item.TeacherPAddress,
                    TcrphotoVM = item.TeacherPhoto,
                    TcrphoneVM = item.TeacherPhoneNo,
                    TcremailVM = item.TeacherEmail,
                };
                //Assinging the counter value  of a element 
                t.QlSLVM = Count;
                Count++;

                //Add the Teacher to  the list
                tv.Add(t);

            }
            //Pass the List to the View
            var List= await  tv.ToPagedListAsync(Page, 5);
            return View(List);
        }
        public async Task<IActionResult>TeacherDetails(Int64 id)
        {
            var tcr = await _context.Teacher.Where(s => s.TeacherId == id).ToListAsync();
            if (tcr.Count != 0)
            {
                var doc = await _context.TeacherDocSubmitted.Where(s => s.TeacherId == id).ToListAsync();
                if (doc.Count != 0)
                {
                    var ql = await _context.TeacherQualification.Where(s => s.TeacherId == id).ToListAsync();
                    if (ql.Count != 0)
                    {

                        var query = (from t in tcr
                                     join d in doc on t.TeacherId equals d.TeacherId
                                     join q in ql on t.TeacherId equals q.TeacherId
                                     where t.TeacherId == id
                                     select new
                                     {
                                         //Teacher Information
                                         Tcrid = t.TeacherId,
                                         Tcrname = t.TeacherName,
                                         Tcrdesig = t.TeacherDesignation,
                                         Tcrdob = t.TeacherDOB,
                                         Tcrbg = t.TeacherBG,
                                         Tcrgender = t.TeacherGender,
                                         Tcrjd = t.TeacherJD,
                                         Tcrnation = t.TeacherNationality,
                                         Tcrcaddrss = t.TeacherCAddress,
                                         Tcrpaddrss = t.TeacherPAddress,
                                         Tcrphoto = t.TeacherPhoto,
                                         Tcrphone = t.TeacherPhoneNo,
                                         Tcremail = t.TeacherEmail,

                                         //Teacher Document Submission
                                         Tcrdocsubsscmark = d.SSCMarksheet,
                                         Tcrdocsubssccrtfct = d.SSCCertificate,
                                         Tcrdocsubhscmark = d.HSCMarksheet,
                                         Tcrdocsubhsccrtfct = d.HSCCertificate,
                                         Tcrdocsubhonsmark = d.HonsMarksheet,
                                         Tcrdocsubhonscrtfct = d.HonsCertificate,

                                         //Teacher Qualifiaction


                                         Tcrqsscyear = q.SSCYear,
                                         Tcrqhscyear = q.HSCYear,
                                         Tcrqhonsyear = q.HonsYear,
                                         Tcrqsscgrade = q.SSCGrade,
                                         Tcrqhscgrade = q.HSCGrade,
                                         Tcrqhonsgrade = q.HonsGrade,
                                         Tcrqsscins = q.SSCInstitute,
                                         Tcrqhscins = q.HonsInstitute,
                                         Tcrqhonsins = q.HonsInstitute,


                                     }).FirstOrDefault();






                        TeacherVm tr = new TeacherVm()
                        {
                            TcridVM = query.Tcrid,
                            TcrnameVM = query.Tcrname,
                            TcrdesigVM = query.Tcrdesig,
                            TcrdobVM = query.Tcrdob,
                            TcrbgVM = query.Tcrbg,
                            TcrgenderVM = query.Tcrgender,
                            TcrjdVM = query.Tcrjd,
                            TcrnationVM = query.Tcrnation,
                            TcrcaddrssVM = query.Tcrcaddrss,
                            TcrpaddrssVM = query.Tcrpaddrss,
                            TcrphotoVM = query.Tcrphoto,
                            TcrphoneVM = query.Tcrphone,
                            TcremailVM = query.Tcremail,
                            //Teacher Document Submission 
                            TcrdocsubsscmarkVM = query.Tcrdocsubsscmark,
                            TcrdocsubssccrtfctVM = query.Tcrdocsubssccrtfct,
                            TcrdocsubhscmarkVM = query.Tcrdocsubhscmark,
                            TcrdocsubhsccrtfctVM = query.Tcrdocsubhsccrtfct,
                            TcrdocsubhonsmarkVM = query.Tcrdocsubhonsmark,
                            TcrdocsubhonscrtfctVM = query.Tcrdocsubhonscrtfct,

                            //Teacher Qualifiaction Submission 
                            TcrqsscyearVM = query.Tcrqsscyear,
                            TcrqhscyearVM = query.Tcrqhscyear,
                            TcrqhonsyearVM = query.Tcrqhonsyear,
                            TcrqsscgradeVM = query.Tcrqsscgrade,
                            TcrqhscgradeVM = query.Tcrqhscgrade,
                            TcrqhonsgradeVM = query.Tcrqhonsgrade,
                            TcrqsscinsVM = query.Tcrqsscins,
                            TcrqhscinsVM = query.Tcrqhscins,
                            TcrqhonsinsVM = query.Tcrqhonsins,

                        };
                        return View(tr);
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }

                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        public async Task<IActionResult> UpdateTeacher(Int64 id)
        {
            var tcr = await _context.Teacher.Where(s => s.TeacherId == id).ToListAsync();
            if (tcr.Count != 0)
            {
                var doc = await _context.TeacherDocSubmitted.Where(s => s.TeacherId == id).ToListAsync();
                if (doc.Count != 0)
                {
                    var ql = await _context.TeacherQualification.Where(s => s.TeacherId == id).ToListAsync();
                    if (ql.Count != 0)
                    {

                        var query = (from t in tcr
                                     join d in doc on t.TeacherId equals d.TeacherId
                                     join q in ql on t.TeacherId equals q.TeacherId
                                     where t.TeacherId == id
                                     select new
                                     {
                                         //Teacher Information
                                         Tcrid = t.TeacherId,
                                         Tcrname = t.TeacherName,
                                         Tcrdesig = t.TeacherDesignation,
                                         Tcrdob = t.TeacherDOB,
                                         Tcrbg = t.TeacherBG,
                                         Tcrgender = t.TeacherGender,
                                         Tcrjd = t.TeacherJD,
                                         Tcrnation = t.TeacherNationality,
                                         Tcrcaddrss = t.TeacherCAddress,
                                         Tcrpaddrss = t.TeacherPAddress,
                                         Tcrphoto = t.TeacherPhoto,
                                         Tcrphone = t.TeacherPhoneNo,
                                         Tcremail = t.TeacherEmail,

                                         //Teacher Document Submission
                                         Tcrdocsubsscmark = d.SSCMarksheet,
                                         Tcrdocsubssccrtfct = d.SSCCertificate,
                                         Tcrdocsubhscmark = d.HSCMarksheet,
                                         Tcrdocsubhsccrtfct = d.HSCCertificate,
                                         Tcrdocsubhonsmark = d.HonsMarksheet,
                                         Tcrdocsubhonscrtfct = d.HonsCertificate,
                                         TcrDocSL = d.TeacherDocSubmittedId,

                                         //Teacher Qualifiaction


                                         Tcrqsscyear = q.SSCYear,
                                         Tcrqhscyear = q.HSCYear,
                                         Tcrqhonsyear = q.HonsYear,
                                         Tcrqsscgrade = q.SSCGrade,
                                         Tcrqhscgrade = q.HSCGrade,
                                         Tcrqhonsgrade = q.HonsGrade,
                                         Tcrqsscins = q.SSCInstitute,
                                         Tcrqhscins = q.HonsInstitute,
                                         Tcrqhonsins = q.HonsInstitute,
                                         TcrQSL = q.TeacherQualificationId


                                     }).FirstOrDefault();


                        TeacherVm tr = new TeacherVm()
                        {
                            TcridVM = query.Tcrid,
                            TcrnameVM = query.Tcrname,
                            TcrdesigVM = query.Tcrdesig,
                            TcrdobVM = query.Tcrdob,
                            TcrbgVM = query.Tcrbg,
                            TcrgenderVM = query.Tcrgender,
                            TcrjdVM = query.Tcrjd,
                            TcrnationVM = query.Tcrnation,
                            TcrcaddrssVM = query.Tcrcaddrss,
                            TcrpaddrssVM = query.Tcrpaddrss,
                            TcrphotoVM = query.Tcrphoto,
                            TcrphoneVM = query.Tcrphone,
                            TcremailVM = query.Tcremail,
                            //Teacher Document Submission 
                            TcrdocsubsscmarkVM = query.Tcrdocsubsscmark,
                            TcrdocsubssccrtfctVM = query.Tcrdocsubssccrtfct,
                            TcrdocsubhscmarkVM = query.Tcrdocsubhscmark,
                            TcrdocsubhsccrtfctVM = query.Tcrdocsubhsccrtfct,
                            TcrdocsubhonsmarkVM = query.Tcrdocsubhonsmark,
                            TcrdocsubhonscrtfctVM = query.Tcrdocsubhonscrtfct,
                            DocSLVM = query.TcrDocSL,

                            //Teacher Qualifiaction Submission 
                            TcrqsscyearVM = query.Tcrqsscyear,
                            TcrqhscyearVM = query.Tcrqhscyear,
                            TcrqhonsyearVM = query.Tcrqhonsyear,
                            TcrqsscgradeVM = query.Tcrqsscgrade,
                            TcrqhscgradeVM = query.Tcrqhscgrade,
                            TcrqhonsgradeVM = query.Tcrqhonsgrade,
                            TcrqsscinsVM = query.Tcrqsscins,
                            TcrqhscinsVM = query.Tcrqhscins,
                            TcrqhonsinsVM = query.Tcrqhonsins,
                            QlSLVM = query.TcrQSL,

                        };
                        return View(tr);
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }

                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTeacher(TeacherVm tr, IFormFile image)
        {
            if (ModelState.IsValid)
            {


                Teacher tchr = new Teacher()
                {
                    TeacherId = tr.TcridVM,
                    TeacherName = tr.TcrnameVM,
                    TeacherBG = tr.TcrbgVM,
                    TeacherEmail = tr.TcremailVM,
                    TeacherCAddress = tr.TcrcaddrssVM,
                    TeacherPAddress = tr.TcrpaddrssVM,
                    TeacherDOB = tr.TcrdobVM,
                    TeacherGender = tr.TcrgenderVM,
                    TeacherNationality = tr.TcrnationVM,
                    TeacherPhoneNo = tr.TcrphoneVM,
                    TeacherJD = tr.TcrjdVM,
                    TeacherPhoto = tr.TcrphotoVM,
                    TeacherDesignation = tr.TcrdesigVM

                };

                //if (image == null)
                //{
                //    if (tr.TcrphotoVM != null)
                //    {
                //        var path = Path.Combine(_imagepath.WebRootPath + "/images");
                //        string filepath = Path.Combine(path, tr.TcrphotoVM);
                //        System.IO.File.Delete(filepath);
                //        tchr.TeacherPhoto = null;
                //    }

                //}
                string un = null;
                if (image != null)
                {
                    if (tr.TcrphotoVM != null)
                    {
                        /// Delete Existing Image
                        var path = Path.Combine(_imagepath.WebRootPath + "/images");
                        string filepath = Path.Combine(path,tr.TcrphotoVM);
                        if(System.IO.File.Exists(filepath))
                        {
                            System.IO.File.Delete(filepath);
                        }
                       
                        tchr.TeacherPhoto = null;
                        path = null;
                        un = null;
                        filepath = null;

                        ///Update Image
                        path = Path.Combine(_imagepath.WebRootPath + "/images");
                        un = Guid.NewGuid().ToString() + "_" + image.FileName;
                        filepath = Path.Combine(path, un);
                        using (var file= new FileStream(filepath, FileMode.Create))
                        {
                            await image.CopyToAsync(file);

                        }
                       
                        tchr.TeacherPhoto = un;
                    }
                    else
                    {
                        var path = Path.Combine(_imagepath.WebRootPath + "/images");
                        un = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string filepath = Path.Combine(path, un);
                        using (var file = new FileStream(filepath, FileMode.Create))
                        {
                            await image.CopyToAsync(file);

                        }
                        tchr.TeacherPhoto = un;
                    }
                }


                _context.Teacher.Update(tchr);
                await _context.SaveChangesAsync();


                ///Save the Document Information to the Database

                TeacherDocSubmitted doc = new TeacherDocSubmitted()
                {
                    TeacherId = tr.TcridVM,
                    TeacherDocSubmittedId = tr.DocSLVM,
                    SSCCertificate = tr.TcrdocsubhsccrtfctVM,
                    SSCMarksheet = tr.TcrdocsubsscmarkVM,
                    HSCCertificate = tr.TcrdocsubhsccrtfctVM,
                    HSCMarksheet = tr.TcrdocsubhscmarkVM,
                    HonsCertificate = tr.TcrdocsubhonscrtfctVM,
                    HonsMarksheet = tr.TcrdocsubhonsmarkVM
                };

                _context.TeacherDocSubmitted.Update(doc);
                await _context.SaveChangesAsync();

                TeacherQualification tq = new TeacherQualification()
                {
                    TeacherQualificationId = tr.QlSLVM,
                    TeacherId = tr.TcridVM,
                    SSCYear = tr.TcrqsscyearVM,
                    SSCGrade = tr.TcrqsscgradeVM,
                    SSCInstitute = tr.TcrqsscinsVM,
                    HSCYear = tr.TcrqhscyearVM,
                    HSCGrade = tr.TcrqhscgradeVM,
                    HSCInstitute = tr.TcrqhscinsVM,
                    HonsYear = tr.TcrqhonsyearVM,
                    HonsGrade = tr.TcrqhonsgradeVM,
                    HonsInstitute = tr.TcrqhonsinsVM
                };
                _context.TeacherQualification.Update(tq);
                await _context.SaveChangesAsync();



                return RedirectToAction("TeacherList");

            }
            else
            {  
                return View();
            }

           

        }


        public IActionResult AddStudent()
        {
            

            var i = _context.Class.ToList();
          
           
            ViewBag.Class = i;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentVm std ,IFormFile image)
        {
            if (ModelState.IsValid && std.stdntclsVM != 0)
            {


                //GEt the List of class
                var i = _context.Class.ToList();
                //Insert a new element in position 0
                i.Insert(0, new Class { ClassId = 0, ClassName = "Select" });
                ViewBag.Class = i;
                // Create a student and save to the database

                if (std.stdntclsVM == 0)
                {
                    ViewBag.Message = "Please enter a valid class";
                    return View();
                }

                var sectioncapacity = await _context.SectionCapacity.AsNoTracking().Where(s => s.ClassId == std.stdntclsVM).ToListAsync();
                int finalsection = 0;
                for (int k = 0; k < sectioncapacity.Count; k++)
                {
                    var secid = sectioncapacity[k].SectionId;
                    var seccapacity = sectioncapacity[k].Capacity;
                    var countStudent = await _context.Student.AsNoTracking().Where(s => s.SectionId == secid).ToListAsync();
                    if (seccapacity > countStudent.Count)
                    {
                        finalsection = secid;
                        break;
                    }

                }



                Student st = new Student()
                {
                    StudentId = std.stdntidVM,
                    StudentName = std.stdntnameVM,
                    StudentCAddress = std.stdntcaddrssVM,
                    StudentPAddress = std.stdntpaddrssVM,
                    StudentNationality = std.stdntnationVM,
                    StudentDOB = std.dobVM,
                    StudentGender = std.stdntgenderVM,
                    DateOfAdmission = DateTime.Now.Date,
                    ClassId = std.stdntclsVM,
                    StudentPhoto = null,
                    Year = DateTime.Now.Year
                };
                st.SectionId = finalsection;

                string un = null;
                if (image != null)
                {
                    var path = Path.Combine(_imagepath.WebRootPath + "/images");
                    un = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filepath = Path.Combine(path, un);
                    using (var file = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(file);

                    }

                    st.StudentPhoto = un;
                }
                _context.Student.Add(st);
                await _context.SaveChangesAsync();
                //Get the id of saved student

                var StudentId = st.StudentId;
                //Create guradian info for that student and save  it
                GuardianInfo gi = new GuardianInfo()
                {
                    GuardianInfoId = 0,
                    StudentId = StudentId,
                    GNameF = std.gnameFVM,
                    GNameM = std.gnameMVM,
                    GPhoneF = std.gphoneFVM,
                    GPhoneM = std.gphoneMVM,
                    GEmailF = std.gemailFVM,
                    GEmailM = std.gemailMVM,
                    GOccupationF = std.goccupationFVM,
                    GOccupationM = std.goccupationMVM,
                    GOrganisationF = std.gorganisationFVM,
                    GOrganisationM = std.gorganisationMVM,
                    GDesignationF = std.gdesignationFVM,
                    GDesignationM = std.gdesignationsMVM


                };
                _context.GuardianInfo.Add(gi);
                await _context.SaveChangesAsync();
                // Create random password 
                PasswordGenerator ps = new PasswordGenerator();
                //Insert the login information for student login and save
                Login lg = new Login()
                {
                    LoginId = 0,
                    Username = StudentId.ToString(),
                    Password = ps.RandomPassword(),
                    FirstLoginStatus = true,
                    ActiveStatus = true,
                    RoleId = 1,
                    DistinguishId = StudentId,
                };
                lg.CurrentPassword = lg.Password;
                _context.Login.Add(lg);
                await _context.SaveChangesAsync();

                //Clear the modelstate
                ModelState.Clear();

                
                return View();
            }
            else
            {
                //Get the List of class
                var i = _context.Class.ToList();
                //insert a new element in position 0
                i.Insert(0, new Class { ClassId = 0, ClassName = "Select" });
                ViewBag.Class = i;
                return View();
            }
        }

        public async Task< IActionResult> StudentList(int Page = 1)
        {
            Int64 Count = 1;
            List<Student> st = _context.Student.ToList();
            List<Class> cs = _context.Class.ToList();

            var query = from s in st
                        join c in cs on s.ClassId equals c.ClassId
                        select new
                        {
                            stdntidVM = s.StudentId,
                            stdntnameVM = s.StudentName,
                            stdntclsVM = c.ClassName,
                            stdntgenderVM = s.StudentGender,
                            stdntphotoVM = s.StudentPhoto,
                            stdntcaddrssVM = s.StudentCAddress,
                            stdntpaddrssVM = s.StudentPAddress,
                            stdntdoaVM = s.DateOfAdmission,
                            stdntnationVM = s.StudentNationality,
                            stdntyearVM = s.Year,
                            dobVM = s.StudentDOB,
                        };
           
            if ( query==null) { return RedirectToAction("Error"); }

            List<StudentVm> sv = new List<StudentVm>();
            foreach (var item in query)
            {

                StudentVm s = new StudentVm()
                {

                    stdntidVM = item.stdntidVM,
                    stdntnameVM = item.stdntnameVM,
                    stdntclsNameVM = item.stdntclsVM,
                    stdntgenderVM = item.stdntgenderVM,
                    stdntphotoVM = item.stdntphotoVM,
                    stdntcaddrssVM = item.stdntcaddrssVM,
                    stdntpaddrssVM = item.stdntpaddrssVM,
                    stdntdoaVM = item.stdntdoaVM,
                    stdntnationVM = item.stdntnationVM,
                    stdntyearVM = item.stdntyearVM,
                    dobVM = item.dobVM,

                };
                //Assinging the counter value  of a element 
                s.Serial = Count;
                Count++;
                //Add the Student to  the list
                sv.Add(s);

            }
            //Pass the List to the View


            var List = await sv.ToPagedListAsync(Page, 5);
            return View(List);
        }
        public async Task<IActionResult> UpdateStudent(Int64 id)
        {

          
            //GEt the List of class
            var i = _context.Class.ToList();

            ViewBag.Class = i;
            //Get the specific student 
            var std = await _context.Student.Where(s => s.StudentId == id).ToListAsync();

            var cls = await _context.Class.AsNoTracking().ToListAsync();

            var grd = await _context.GuardianInfo.Where(a => a.StudentId == id).ToListAsync();
            if(std.Count()==0|| cls.Count() == 0 || grd.Count() == 0) { return RedirectToAction("Error"); }

            var query = (from st in std
                         join g in grd on st.StudentId equals g.StudentId

                         where st.StudentId == g.StudentId
                         select new
                         {
                             stdntid = st.StudentId,
                             stdntname = st.StudentName,
                             stdntcls = st.ClassId,
                             stdntgender = st.StudentGender,
                             stdntphoto = st.StudentPhoto,
                             stdntcaddrss = st.StudentCAddress,
                             stdntpaddrss = st.StudentPAddress,
                             stdntdoa = st.DateOfAdmission,
                             stdntnation = st.StudentNationality,
                             stdntyear = st.Year,
                             dob = st.StudentDOB,
                             stdntbg = st.StudentBG,
                             sectionid=st.SectionId,

                             //Guardian Info
                             GuardianInfoId=g.GuardianInfoId,
                             GNameMA = g.GNameF,
                             GNameMO = g.GNameM,
                             GPhoneFA = g.GPhoneF,
                             GPhoneMO = g.GPhoneM,
                             GEmailMO = g.GEmailM,
                             GEmailFA = g.GEmailF,
                             GOccupationFA = g.GOccupationF,
                             GOccupationMO = g.GOccupationM,
                             GOrganisationFA = g.GOrganisationF,
                             GOrganisationMO = g.GOrganisationM,
                             GDesignationFA = g.GDesignationF,
                             GDesignationMO = g.GDesignationM
                         }).FirstOrDefault();

            StudentVm svm = new StudentVm()
            {
                stdntnameVM = query.stdntname,
                stdntidVM = query.stdntid,
                stdntclsVM = query.stdntcls,
                stdntgenderVM = query.stdntgender,
                stdntphotoVM = query.stdntphoto,
                stdntcaddrssVM = query.stdntcaddrss,
                stdntpaddrssVM = query.stdntpaddrss,
                stdntdoaVM = query.stdntdoa,
                stdntnationVM = query.stdntnation,
                stdntyearVM = query.stdntyear,
                dobVM = query.dob,
                SectionId=query.sectionid,
                ///Guardian info
                Serial=query.GuardianInfoId,
                gnameFVM = query.GNameMA,
                gnameMVM = query.GNameMA,
                gphoneFVM = query.GPhoneFA,
                gphoneMVM = query.GPhoneMO,
                gemailFVM = query.GEmailFA,
                gemailMVM = query.GEmailMO,
                goccupationFVM = query.GOccupationFA,
                goccupationMVM = query.GOccupationMO,
                gorganisationFVM = query.GOrganisationFA,
                gorganisationMVM = query.GOrganisationMO,
                gdesignationFVM = query.GDesignationFA,
                gdesignationsMVM = query.GDesignationMO
            };


            return View(svm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStudent(StudentVm std, IFormFile image)
        {
                //GEt the List of class
                var i = _context.Class.ToList();

                ViewBag.Class = i;
            if (ModelState.IsValid )
            {

                // Update a student and save to the database
                Student st = new Student()
                {
                    StudentId = std.stdntidVM,
                    StudentName = std.stdntnameVM,
                    StudentCAddress = std.stdntcaddrssVM,
                    StudentPAddress = std.stdntpaddrssVM,
                    StudentNationality = std.stdntnationVM,
                    StudentDOB = std.dobVM,
                    StudentGender = std.stdntgenderVM,
                    DateOfAdmission = std.stdntdoaVM,
                    ClassId = std.stdntclsVM,
                    StudentPhoto = std.stdntphotoVM,
                    Year = std.stdntyearVM,
                    SectionId=std.SectionId
                };

                string un = null;
                if (image != null)
                {
                    if (st.StudentPhoto != null)
                    {
                        /// Delete Existing Image
                        var path = Path.Combine(_imagepath.WebRootPath + "/images");
                        string filepath = Path.Combine(path, st.StudentPhoto);
                        if (System.IO.File.Exists(filepath))
                        {
                            System.IO.File.Delete(filepath);
                        }

                        st.StudentPhoto = null;
                        path = null;
                        un = null;
                        filepath = null;

                        ///Update Image
                        path = Path.Combine(_imagepath.WebRootPath + "/images");
                        un = Guid.NewGuid().ToString() + "_" + image.FileName;
                        filepath = Path.Combine(path, un);
                        using (var file = new FileStream(filepath, FileMode.Create))
                        {
                            await image.CopyToAsync(file);

                        }

                        st.StudentPhoto = un;
                    }
                    else
                    {
                        var path = Path.Combine(_imagepath.WebRootPath + "/images");
                        un = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string filepath = Path.Combine(path, un);
                        using (var file = new FileStream(filepath, FileMode.Create))
                        {
                            await image.CopyToAsync(file);

                        }
                        st.StudentPhoto = un;
                    }
                }
                _context.Student.Update(st);
                await _context.SaveChangesAsync();



                //Create guradian info for that student and save  it
                GuardianInfo gi = new GuardianInfo()
                {
                    GuardianInfoId = std.Serial,
                    StudentId = std.stdntidVM,
                    GNameF = std.gnameFVM,
                    GNameM = std.gnameMVM,
                    GPhoneF = std.gphoneFVM,
                    GPhoneM = std.gphoneMVM,
                    GEmailF = std.gemailFVM,
                    GEmailM = std.gemailMVM,
                    GOccupationF = std.goccupationFVM,
                    GOccupationM = std.goccupationMVM,
                    GOrganisationF = std.gorganisationFVM,
                    GOrganisationM = std.gorganisationMVM,
                    GDesignationF = std.gdesignationFVM,
                    GDesignationM = std.gdesignationsMVM


                };
                _context.GuardianInfo.Update(gi);
                await _context.SaveChangesAsync();



                return RedirectToAction("StudentList");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> StudentDetails(Int64 id)
        {

            var cls = await _context.Class.AsNoTracking().ToListAsync();

            var std = _context.Student.Where(s => s.StudentId == id).ToList();

            if (std.Count != 0)
            {
                var grd = _context.GuardianInfo.Where(a => a.StudentId == id).ToList();
                if (grd.Count != 0)
                {

                    var query = (from st in std
                                 join g in grd on st.StudentId equals g.StudentId
                                 join c in cls on st.ClassId equals c.ClassId
                                 where st.StudentId == g.StudentId
                                 select new
                                 {
                                     stdntid = st.StudentId,
                                     stdntname = st.StudentName,
                                     stdntcls = c.ClassName,
                                     stdntgender = st.StudentGender,
                                     stdntphoto = st.StudentPhoto,
                                     stdntcaddrss = st.StudentCAddress,
                                     stdntpaddrss = st.StudentPAddress,
                                     stdntdoa = st.DateOfAdmission,
                                     stdntnation = st.StudentNationality,
                                     stdntyear = st.Year,
                                     dob = st.StudentDOB,
                                     stdntbg = st.StudentBG,

                                     //Guardian Info

                                     GNameMA = g.GNameF,
                                     GNameMO = g.GNameM,
                                     GPhoneFA = g.GPhoneF,
                                     GPhoneMO = g.GPhoneM,
                                     GEmailMO = g.GEmailM,
                                     GEmailFA = g.GEmailF,
                                     GOccupationFA = g.GOccupationF,
                                     GOccupationMO = g.GOccupationM,
                                     GOrganisationFA = g.GOrganisationF,
                                     GOrganisationMO = g.GOrganisationM,
                                     GDesignationFA = g.GDesignationF,
                                     GDesignationMO = g.GDesignationM
                                 }).FirstOrDefault();

                    StudentVm svm = new StudentVm()
                    {
                        stdntnameVM = query.stdntname,
                        stdntidVM = query.stdntid,
                        stdntclsNameVM = query.stdntcls,
                        stdntgenderVM = query.stdntgender,
                        stdntphotoVM = query.stdntphoto,
                        stdntcaddrssVM = query.stdntcaddrss,
                        stdntpaddrssVM = query.stdntpaddrss,
                        stdntdoaVM = query.stdntdoa,
                        stdntnationVM = query.stdntnation,
                        stdntyearVM = query.stdntyear,
                        dobVM = query.dob,
                        ///Guardian info
                        gnameFVM = query.GNameMA,
                        gnameMVM = query.GNameMA,
                        gphoneFVM = query.GPhoneFA,
                        gphoneMVM = query.GPhoneMO,
                        gemailFVM = query.GEmailFA,
                        gemailMVM = query.GEmailMO,
                        goccupationFVM = query.GOccupationFA,
                        goccupationMVM = query.GOccupationMO,
                        gorganisationFVM = query.GOrganisationFA,
                        gorganisationMVM = query.GOrganisationMO,
                        gdesignationFVM = query.GDesignationFA,
                        gdesignationsMVM = query.GDesignationMO

                    };

                    return View(svm);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }



        }
        public IActionResult Error()
        { return View(); }

    }
}