using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonoSAR.Models.DB;
using Microsoft.EntityFrameworkCore;
using MonoSAR.Models;
using Microsoft.Extensions.Options;

namespace MonoSAR.Controllers
{
    public class TrainingOfficerController : Controller
    {
        private Models.DB.monosarsqlContext _context;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private IOptions<ApplicationSettings> _applicationOptions;


        public TrainingOfficerController(IConfiguration config, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager, IOptions<ApplicationSettings> options)
        {
            this._context = new Models.DB.monosarsqlContext(config);
            this._config = config;
            this._userManager = usermanager;
            this._applicationOptions = options;
        }

        // GET: TrainingOfficer
        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        [HttpPost]
        public ActionResult Index()
        {
            var query = (from tsc in _context.TrainingClassStudent
                         orderby tsc.Created descending, tsc.TrainingClass.TrainingDate descending
                         select tsc).Take(100);

            //Explicit loading because EF Core isn't lazy
            _context.TrainingClassStudent.Include(x => x.TrainingClassStudentMember).Load();
            _context.TrainingClassStudent.Include(x => x.TrainingClass).ThenInclude(y=>y.Training).Load();
                        
            //converting from data models to the view model (dto), the conversions happen in the view model / dto's constructor
            Models.Training.TrainingClassStudentSummary model = new Models.Training.TrainingClassStudentSummary(query);
            
            return View(model); 
        }

        // GET: TrainingOfficer
        [Authorize(Roles = "Admin,Training")]
        public ActionResult RecordTrainingOccurrence()
        {

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Training")]
        public ViewResult CreateOccurrence(Models.Training.TrainingClassInsert trainingClassInsert)
        {
            Models.DB.TrainingClass trainingClass = new TrainingClass(); //the class itself
            List<Models.Training.TrainingClassParticipant> students = new List<Models.Training.TrainingClassParticipant>(); //the students' participation in the above training class
            List<Models.Training.TrainingClassParticipant> instructors = new List<Models.Training.TrainingClassParticipant>(); //the instructors' participation in the above training class
            var x = HttpContext.Request.Form;
            var thanks = new Models.Training.TrainingClassInsertConfirmation();
            
            Int32 i = 0;
            Int32 j = 0;

            //gross, but looping through the form data to find students/instructors/hours, not using binding for this
            foreach (var item in x)
            {
                if (item.Key == "member")
                {
                    foreach (var y in item.Value)
                    {
                        //gets the memberid
                        Models.Training.TrainingClassParticipant topi = new Models.Training.TrainingClassParticipant();
                        String maybememberid = y.ToString();
                        topi.MemberID = Int32.Parse(y.ToString());
                        
                        //get the hours
                        string maybeHours = x["hours"][i];
                        topi.Hours = Decimal.Parse(maybeHours);

                        //keeping a counter so (above) we know how to find the hours by index. ie: for the third member, we look for the third set of hours.
                        students.Add(topi);
                        i = i + 1;
                    }
                }

                if (item.Key == "instructor")
                {
                    foreach (var y in item.Value)
                    {
                        //gets the memberid
                        Models.Training.TrainingClassParticipant topi = new Models.Training.TrainingClassParticipant();
                        String maybememberid = y.ToString();
                        topi.MemberID = Int32.Parse(y.ToString());

                        //get the hours
                        string maybeHours = x["ihours"][j];
                        topi.Hours = Decimal.Parse(maybeHours);

                        //keeping a counter so (above) we know how to find the hours by index. ie: for the third member, we look for the third set of hours.
                        instructors.Add(topi);
                        j = j + 1;
                    }
                }
            }


            trainingClass.Created = DateTime.UtcNow;
            trainingClass.TrainingDate = trainingClassInsert.TrainingDate;
            trainingClass.TrainingId = trainingClassInsert.TrainingID;
            trainingClass.TrainingClassStudent = new List<TrainingClassStudent>();
            trainingClass.TrainingClassInstructor = new List<TrainingClassInstructor>();


            //parse the form and grab the instructors and students, along with their hours objects to store
            foreach (var item in students)
            {

                Models.DB.TrainingClassStudent trainingClassStudent = new TrainingClassStudent();

                trainingClassStudent.Created = DateTime.UtcNow;
                trainingClassStudent.TrainingClassStudentMemberId = item.MemberID;
                trainingClassStudent.TrainingClassStudentHours = item.Hours;

                trainingClass.TrainingClassStudent.Add(trainingClassStudent);

            }

            foreach (var item in instructors)
            {
                Models.DB.TrainingClassInstructor trainingClassInstructor = new TrainingClassInstructor();

                trainingClassInstructor.Created = DateTime.UtcNow;
                trainingClassInstructor.TrainingClassInstructorMemberId = item.MemberID;
                trainingClassInstructor.TrainingClassStudentHours = item.Hours;

                trainingClass.TrainingClassInstructor.Add(trainingClassInstructor);
            }


            try
            {
                _context.Add(trainingClass);
                _context.SaveChanges();
            }
            catch (Exception exc)
            {
                throw new Exception("Error saving new class (EF): " + exc.ToString());
            }

            
            var hours = students.Sum(item => item.Hours).ToString();
            var people = students.Count.ToString() ;
            
            thanks.Message = "Great work, your team has added " + hours + " hours of training across " + people + " members."   ;
            
            return View("TrainingOccurrenceInsertConfirmation", thanks);
        }

        // GET: TrainingOfficer/Details/5
        [Authorize(Roles = "Admin,Training")]
        public ActionResult ViewMember(int id)
        {
            var query = (from m in _context.Member
                         where m.MemberId == id
                         select m).FirstOrDefault();

            if (query == null)
            { throw new Exception("Unable to locate member."); }

            _context.Member.Include(x => x.MemberCertification).ThenInclude(y => y.Certification).Load();
            _context.Member.Include(x => x.MemberCpr).ThenInclude(y => y.Cpr).Load();
            _context.Member.Include(x => x.MemberMedical).ThenInclude(y => y.Medical).Load();
            _context.Member.Include(x => x.Capacity).Load();
            _context.Member.Include(x => x.TrainingClassStudent).ThenInclude(y => y.TrainingClass).ThenInclude(z=>z.Training).Load();

            var model = new Models.Membership.MemberSummaryItem(query, _applicationOptions, _config);
            
            return View(model);
        }

        

        // GET: TrainingOfficer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingOfficer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainingOfficer/Edit/5
        [Authorize(Roles = "Admin,Training")]
        public ActionResult Edit(int id)
        {

            var query = (from tsc in _context.TrainingClassStudent
                         where tsc.TrainingClassStudentId == id
                         select tsc).FirstOrDefault();

            //Explicit loading because EF Core isn't lazy
            _context.TrainingClassStudent.Include(x => x.TrainingClassStudentMember).Load();
            _context.TrainingClassStudent.Include(x => x.TrainingClass).ThenInclude(y => y.Training).Load();

            //converting from data models to the view model (dto), the conversions happen in the view model / dto's constructor
            Models.Training.TrainingClassStudentSummaryItem model = new Models.Training.TrainingClassStudentSummaryItem(query);
            
            return View(model);

        }

        // POST: TrainingOfficer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Training")]
        public ActionResult Edit(Models.Training.TrainingClassStudentSummaryItem viewModel)
        {
            // only updating hours and date at the moment

            try {

                var query = (from tsc in _context.TrainingClassStudent
                             where tsc.TrainingClassStudentId == viewModel.TrainingClassStudentID
                             select tsc).FirstOrDefault();

                query.TrainingClassStudentHours = viewModel.Hours;
                
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        // POST: TrainingOfficer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Training")]
        public ActionResult Delete(Models.Training.TrainingClassStudentSummaryItem viewModel)
        {
            try
            {
                var del = new Models.DB.TrainingClassStudent { TrainingClassStudentId = viewModel.TrainingClassStudentID };
                _context.TrainingClassStudent.Attach(del);
                _context.Remove(del);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Training")]
        public ActionResult CreateMedicalOccurrence(Models.Training.MemberMedicalInsert model)
        {
            Models.DB.MemberMedical memberMedical = new MemberMedical();

            memberMedical.Created = DateTime.UtcNow;
            memberMedical.Expiration = model.Expiration;
            memberMedical.Issued = model.Issued;
            memberMedical.MedicalId = model.MedicalID;
            memberMedical.MemberId = model.MemberID;

            _context.Add(memberMedical);
            _context.SaveChanges();

            return Redirect("/TrainingOfficer/ViewMember/" + model.MemberID);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Training")]
        public ActionResult CreateCprOccurrence(Models.Training.MemberCPRInsert model)
        {
            Models.DB.MemberCpr memberCpr = new MemberCpr();

            memberCpr.Created = DateTime.UtcNow;
            memberCpr.Expiration = model.Expiration;
            memberCpr.Issued = model.Issued;
            memberCpr.Cprid = model.CPRID;
            memberCpr.MemberId = model.MemberID;

            _context.Add(memberCpr);
            _context.SaveChanges();

            return Redirect("/TrainingOfficer/ViewMember/" + model.MemberID);

        }

        [Authorize(Roles = "Admin,Training")]
        public ActionResult RecordMedical()
        {
            var query = (from m in _context.Member
                         orderby m.LastName
                         select m).ToList();

            Models.Training.MemberMedicalInsert model = new Models.Training.MemberMedicalInsert();
            model.MemberStubs = new Models.Membership.MemberStubs(query);

            var meds = (from med in _context.Medical
                        orderby med.RankOrder
                        select med).ToList();

            model.MedicalStubs = new Models.Training.MedicalStubs(meds);

            model.Issued = DateTime.Now;
            model.Expiration = DateTime.Now.AddYears(2);


            return View(model);
        }
        [Authorize(Roles = "Admin,Training")]
        public ActionResult RecordCPR()
        {
            var query = (from m in _context.Member
                         orderby m.LastName
                         select m).ToList();

            Models.Training.MemberCPRInsert model = new Models.Training.MemberCPRInsert();
            model.MemberStubs = new Models.Membership.MemberStubs(query);

            var cprs = (from c in _context.Cpr
                        orderby c.RankOrder
                        select c).ToList();

            model.CPRStumps = new Models.Training.CPRStumps(cprs);

            model.Issued = DateTime.Now;
            model.Expiration = DateTime.Now.AddYears(2);


            return View(model);
        }
    }
}