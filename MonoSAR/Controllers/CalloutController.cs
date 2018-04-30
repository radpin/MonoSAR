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

        [HttpGet]
        [HttpPost]
        public ContentResult GetTwilioMessage(Int32 id)
        {
            //Twilio makes an Http Post request, but get is handy for a human looking at it without postman.

            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            var callout = (from x in _context.Callout where x.CalloutId == id select x).FirstOrDefault();

            //Services.Telephony telephony = new Services.Telephony(_applicationOptions, _config, _context);

            //String messageXmlFormatted = telephony.FormatStringToTwiml(callout.CalloutMessage);// escapeXml(callout.CalloutMessage);
            

            stringBuilder.Append(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            stringBuilder.Append("<Response>");
            stringBuilder.Append(@"<Say voice=""alice"">");
            stringBuilder.Append(callout.CalloutMessage);
            stringBuilder.Append("</Say>");
            stringBuilder.Append("</Response>");

            return this.Content(stringBuilder.ToString(), "text/xml");
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

        private string escapeXml(string s)
        {
            string toxml = s;
            if (!string.IsNullOrEmpty(toxml))
            {
                // replace literal values with entities
                toxml = toxml.Replace("'", "&apos;");
                toxml = toxml.Replace("\"", "&quot;");
                toxml = toxml.Replace(">", "&gt;");
                toxml = toxml.Replace("<", "&lt;");
                toxml = toxml.Replace("&", "&amp;");
            }
            return toxml;
        }
    }
}
