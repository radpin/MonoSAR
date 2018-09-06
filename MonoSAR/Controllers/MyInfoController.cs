using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MonoSAR.Models;

namespace MonoSAR.Controllers
{
    public class MyInfoController : Controller
    {

        private Models.DB.monosarsqlContext _context;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private IOptions<ApplicationSettings> _applicationOptions;

        // GET: MyInfo
        [Authorize]
        public ActionResult Index()
        {
            var loggedInMember = (from x in _context.Member
                         where x.Email.ToLower() == User.Identity.Name.ToLower()
                         select x).FirstOrDefault();

            if (loggedInMember == null)
            { throw new Exception("Email address of logged in user not found in membership data."); }

            _context.Member.Include(x => x.MemberCertification).ThenInclude(y => y.Certification).Load();
            _context.Member.Include(x => x.MemberCpr).ThenInclude(y=>y.Cpr).Load();
            _context.Member.Include(x => x.MemberMedical).ThenInclude(y => y.Medical).Load();
            _context.Member.Include(x => x.Capacity).Load();
            _context.Member.Include(x => x.TrainingClassStudent).ThenInclude(y => y.TrainingClass).ThenInclude(z => z.Training).Load();
            _context.Member.Include(x => x.OperationMember).ThenInclude(y => y.Operation).Load();

            Models.Membership.MemberSummaryItem memberSummaryItem = new Models.Membership.MemberSummaryItem(loggedInMember, _applicationOptions, _config);

            return View(memberSummaryItem);
        }


        public MyInfoController(IConfiguration config, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager, IOptions<ApplicationSettings> options)
        {
            this._context = new Models.DB.monosarsqlContext(config);
            this._config = config;
            this._userManager = usermanager;
            this._applicationOptions = options;
           
        }

        // GET: MyInfo/Edit
        public ActionResult Edit()
        {
            var loggedInMember = (from x in _context.Member
                                  where x.Email.ToLower() == User.Identity.Name.ToLower()
                                  select x).FirstOrDefault();

            if (loggedInMember == null)
            { throw new Exception("Email address of logged in user not found in membership data."); }

            var model = new Models.Membership.MyInfoUpdate(loggedInMember);

            return View(model);
        }

        // POST: MyInfo/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Membership.MyInfoUpdate viewModel)
        {
            try
            {
                var loggedInMember = (from x in _context.Member
                                      where x.Email.ToLower() == User.Identity.Name.ToLower()
                                      select x).FirstOrDefault();

                if (loggedInMember == null)
                { throw new Exception("Email address of logged in user not found in membership data."); }

                loggedInMember.FirstName = viewModel.FirstName;
                loggedInMember.LastName = viewModel.LastName;
                loggedInMember.Address = viewModel.Address;
                loggedInMember.City = viewModel.City;
                loggedInMember.State = viewModel.State;
                loggedInMember.Zipcode = viewModel.Zip;
                loggedInMember.Email = viewModel.Email;
                loggedInMember.PhoneHome = viewModel.PhoneHome ?? String.Empty;
                loggedInMember.PhoneCell = viewModel.PhoneCell ?? String.Empty;
                loggedInMember.PhoneWork = viewModel.PhoneWork ?? String.Empty;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}