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
            _context.Member.Include(x => x.TrainingMember).ThenInclude(y => y.Training).Load();

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

    }
}