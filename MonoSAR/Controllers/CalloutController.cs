using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MonoSAR.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonoSAR.Controllers
{

    public class CalloutController : Controller
    {


        private Models.DB.monosarsqlContext _context;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private IOptions<ApplicationSettings> _applicationOptions;


        public CalloutController(IConfiguration config, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager, IOptions<ApplicationSettings> options)
        {
            this._context = new Models.DB.monosarsqlContext(config);
            this._config = config;
            this._userManager = usermanager;
            this._applicationOptions = options;
        }





        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromForm]string value)
        {
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin,OpsLeader")]
        [HttpPost]
        public ActionResult Start([FromForm]string value)
        {
            Services.Telephony t = new Services.Telephony(_applicationOptions, _config, _context);

            var members = this.memberSummaryItems();
            t.SendCalloutHeadsupSMS(members);

            Models.Callout.CalloutOccurrence model = new Models.Callout.CalloutOccurrence();

            return View(model);
        }

        public ContentResult GetTwilioMessage(Int32 calloutid)
        {
            //todo: render the xml
            //1) persist the callout message in this controller
            //2) assemble the url (this method). should it return a string? xml? json? etc.
            //3) make the message/twilio call

            return this.Content("dsf", "text/xml");
        }

        [Authorize(Roles = "Admin,OpsLeader")]
        [HttpPost]
        public ActionResult Initiate(Models.Callout.CalloutOccurrence model)
        {
            Services.Telephony t = new Services.Telephony(_applicationOptions, _config, _context);

            //send a text message
            var members = this.memberSummaryItems();
            t.SendCalloutHeadsupSMS(members);
            
            //save the new callout message
            Models.DB.Callout newCallout = new Models.DB.Callout();
            newCallout.Created = DateTime.UtcNow;
            newCallout.CalloutMessage = model.VoiceMessage = model.VoiceMessage;

            _context.Callout.Add(newCallout);
            _context.SaveChanges();

            t.SendCalloutPhoneCallsandSMS(members, newCallout);

            return View();
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        private Models.Membership.MemberSummary memberSummaryItems()
        {
            Models.Membership.MemberSummary memberList = new Models.Membership.MemberSummary();

            var dbmems = (from m in _context.Member
                          orderby m.LastName, m.FirstName
                          select m).ToList();

            _context.Member.Include(x => x.MemberCertification).ThenInclude(y => y.Certification).Load();
            _context.Member.Include(x => x.MemberCpr).ThenInclude(y => y.Cpr).Load();
            _context.Member.Include(x => x.MemberMedical).ThenInclude(y => y.Medical).Load();
            _context.Member.Include(x => x.Capacity).Load();
            _context.Member.Include(x => x.TrainingClassStudent).ThenInclude(y => y.TrainingClass).ThenInclude(z => z.Training).Load();

            foreach (var x in dbmems)
            {
                memberList.Add(new Models.Membership.MemberSummaryItem(x, _applicationOptions, _config));
            }

            return memberList;
        }
    }
}
