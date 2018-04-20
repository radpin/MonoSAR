using Microsoft.Extensions.Configuration;
using MonoSAR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MonoSAR.Services
{
    public class Telephony
    {

        private Models.ApplicationSettings _applicationSettings;
        private Models.DB.monosarsqlContext _context;
        private IConfiguration _config;

        public Telephony(Microsoft.Extensions.Options.IOptions<ApplicationSettings> settings, IConfiguration config, Models.DB.monosarsqlContext context)
        {
            this._applicationSettings = settings.Value;
            this._config = config;
            this._context = context;

            var x = config["sendgridapikey"];
        }

        public void SendCalloutHeadsupSMS(IEnumerable<Models.Membership.MemberSummaryItem> members)
        {
            //for testing the foreach is disabled, and just sending it to me
            this.sendSMS("619-752-0976", "(Testing: Ignore) Possible callout - more information to follow. Please prepare yourself.");

            foreach (var x in members)
            {
                //this.sendSMS(x.PhoneCell, "(Testing: Ignore) Possible callout - more information to follow. Please prepare yourself.");
            }
        }


        private void sendSMS(String numbertotext, String texttosend)
        {
            try
            {
                string accountSid = this._config["twilio-accountsid"];
                string authToken = this._config["twilio-authtoken"];

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber(numbertotext);
                var message = MessageResource.Create(
                    to,
                    from: new PhoneNumber("+" + this._config["twilio-fromnumber"]),
                    body: texttosend);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        /// <summary>
        /// This method is for real-actual callout notifications.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="callout"></param>
        public void SendCalloutPhoneCallsandSMS(IEnumerable<Models.Membership.MemberSummaryItem> members, Models.DB.Callout callout)
        {
            //for testing the foreach is disabled, and just sending it to me
            this.sendVoiceCall("619-752-0976", callout);
            
            foreach (var x in members)
            {
                this.sendSMS(x.PhoneCell, callout.CalloutMessage);
                this.sendVoiceCall(x.PhoneCell, callout);
            }
        }



        private void sendVoiceCall(String numbertocall, Models.DB.Callout callout)
        {
            try
            {
                string accountSid = this._config["twilio-accountsid"];
                string authToken = this._config["twilio-authtoken"];
                string baseurl = _applicationSettings.BaseURL;

                String url = _applicationSettings.BaseURL + "/Callout/GetTwilioMessage/" + callout.CalloutId.ToString();

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber("+16198493742");
                var from = new PhoneNumber("+17602034033");

                var call = CallResource.Create(to,
                                               from,
                                               url: new Uri(url));
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

    }
}
