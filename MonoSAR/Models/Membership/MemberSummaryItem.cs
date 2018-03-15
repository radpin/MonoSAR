using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
/// <summary>
/// This transforms the EF model of "member", and the child properties and collections, into a model for MVC binding. The child objects (medical, certs, cpr, etc) are 
/// have their transformations as part of their constructors. 
/// </summary>
    public class MemberSummaryItem
    {

        private Models.ApplicationSettings _applicationSettings;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private DB.monosarsqlContext _context;

        public MemberSummaryItem(Models.DB.Member dataEntity, Microsoft.Extensions.Options.IOptions<ApplicationSettings> settings, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager, IConfiguration config)
        {
            _applicationSettings = settings.Value;
            _userManager = usermanager;
            this._context = new DB.monosarsqlContext(config);

            this.First = dataEntity.FirstName;
            this.Last = dataEntity.LastName;
            this.HamCallSign = dataEntity.Ham;
            this.Joined = dataEntity.Joined;
            this.PhoneCell = dataEntity.PhoneCell;
            this.PhoneHome = dataEntity.PhoneHome;
            this.PhoneWork = dataEntity.PhoneWork;
            this.Address = dataEntity.Address;
            this.City = dataEntity.City;
            this.Email = dataEntity.Email;

            this.State = dataEntity.State;
            this.ZIP = dataEntity.Zipcode;

            this.buildBeaconCheck(dataEntity);
            this.buildCertification(dataEntity);
            this.buildCPR(dataEntity);
            this.buildMedical(dataEntity);
            this.buildInitialFieldChecks(dataEntity);

            this.Capacity = dataEntity.Capacity.CapacityName;
        }


        private void buildMedical(Models.DB.Member dataItem)
        {
            List<MedicalSummary> medicalSummaries = new List<MedicalSummary>();

            foreach (var item in dataItem.MemberMedical)
            {
                medicalSummaries.Add(new MedicalSummary(item));
            }

            this.MedicalSummaries = medicalSummaries;

            if (dataItem.MemberMedical != null)
            { this.MedicalExpires = dataItem.MemberMedical.OrderByDescending(e => e.Expiration).FirstOrDefault().Expiration; }
        }

        private void buildCPR(Models.DB.Member dataItem)
        {
            List<CPRSummary> cPRSummaries = new List<CPRSummary>();

            foreach (var item in dataItem.MemberCpr)
            {
                cPRSummaries.Add(new CPRSummary(item));
            }

            this.CPRSummaries = cPRSummaries;

            //this one
            if (dataItem.MemberCpr.Count > 0)
            { this.CPRExpires = dataItem.MemberCpr.OrderByDescending( e => e.Expiration).FirstOrDefault().Expiration; }

        }

        private void buildCertification(Models.DB.Member dataItem)
        {
            List<CertificationSummary> certificationSummaries = new List<CertificationSummary>();

            foreach (var item in dataItem.MemberCertification)
            {
                certificationSummaries.Add(new CertificationSummary(item));
            }

            this.CertificationSummaries = certificationSummaries;
        }

        private void buildInitialFieldChecks(Models.DB.Member dataItem)
        {
            //I think because these are being lazy loaded that they can be coverted from _context to simply querying the dataItem (with its lazy loaded properties).
            //a few less data calls, unless EF is smart enough to know that already so querying _context doesn't matter. - eric


            var bv = (from tm in _context.TrainingMember
                     where tm.MemberId == dataItem.MemberId && tm.TrainingId == _applicationSettings.RequiredBVTest
                     select tm).FirstOrDefault();

            var bcc = (from tm in _context.TrainingMember
                          where tm.MemberId == dataItem.MemberId && tm.TrainingId == _applicationSettings.RequiredCandidateBasicClass
                          select tm).FirstOrDefault();

            var ics100 = (from tm in _context.TrainingMember
                       where tm.MemberId == dataItem.MemberId && tm.TrainingId == _applicationSettings.RequiredICS100
                       select tm).FirstOrDefault();

            var ics200 = (from tm in _context.TrainingMember
                          where tm.MemberId == dataItem.MemberId && tm.TrainingId == _applicationSettings.RequiredICS200
                          select tm).FirstOrDefault();

            var pc = (from tm in _context.TrainingMember
                      where tm.MemberId == dataItem.MemberId && tm.TrainingId == _applicationSettings.RequiredPackCheck
                      select tm).FirstOrDefault();

            if (bv != null) { this.IsBuildingVehicleTested = true; }
            if (bcc != null) { this.IsCandidateClass = true; }
            if (ics100 != null) { this.IsICS100 = true; }
            if (ics200 != null) { this.IsICS200 = true; }
            if (pc != null) { this.IsPackChecked = true; }
        }

        private void buildBeaconCheck(Models.DB.Member dataItem)
        {
            var beac = (from tm in _context.TrainingMember
                        where tm.MemberId == dataItem.MemberId && tm.TrainingId == _applicationSettings.RequiredBeaconTest
                        select tm).FirstOrDefault();

            if (beac != null && beac.TrainingDate > DateTime.Now.AddYears(-1))
            { this.IsBeaconExpired = true; }
        }

        private void buildTraining(Models.DB.Member dataItem)
        {
            //this creation should be moved to the TrainingSummaryItem constructor to keep it more consistent with other patterns - eric

            List<Training.TrainingSummaryItem> trainingSummaries = new List<Training.TrainingSummaryItem>();
            

            foreach (var tm in dataItem.TrainingMember)
            {
                Training.TrainingSummaryItem tsi = new Training.TrainingSummaryItem();
                tsi.Created = tm.Created;
                tsi.Hours = tm.TrainingHours;
                tsi.MemberName = tm.Member.LastName;
                tsi.TrainingMemberID = tm.TrainingMemberId;
                tsi.TrainingTitle = tm.Training.TrainingTitle;
                tsi.When = tm.TrainingDate;

                trainingSummaries.Add(tsi);
            }

            this.TrainingSummaries = trainingSummaries;
        }

        public String First { get; set; }
        public String Last { get; set; }
        public String HamCallSign { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZIP { get; set; }
        public String Email { get; set; }
        public DateTime Joined { get; set; }
        public String JoinedPretty { get { return Joined.ToShortDateString(); } }
        public String PhoneHome { get; set; }
        public String PhoneCell { get; set; }
        public String PhoneWork { get; set; }
        public String Capacity { get; set; }

        public DateTime MedicalExpires { get; set; }
        public String MedicalExpiresPretty { get { return MedicalExpires.ToShortDateString(); } }
        public Boolean IsMedicalExpired {

        get {
                if (MedicalExpires > DateTime.Now)
                { return false; }

                return true;
            }
                                        }
        public DateTime CPRExpires { get; set; }
        public String CPRExpiresPretty { get { return CPRExpires.ToShortDateString(); } }
        public Boolean IsCPRExpired
        {

            get
            {
                if (CPRExpires > DateTime.Now)
                { return false; }

                return true;
            }
        }
        public Boolean IsBeaconExpired{get;set;}
        public Boolean IsWinterFieldReady
        {
            get
            {
                if (!IsMedicalExpired && !IsCPRExpired && !IsBeaconExpired)
                { return true; }
                else
                { return false; }
            }
        }
        public Boolean IsSummerFieldReady
        {
            get
            {
                if (!IsMedicalExpired && !IsCPRExpired)
                { return true; }
                else
                { return false; }
            }
        }

        public Boolean IsICS100 { get; set; }
        public Boolean IsICS200 { get; set; }
        public Boolean IsBuildingVehicleTested { get; set; }
        public Boolean IsPackChecked { get; set; }
        public Boolean IsCandidateClass { get; set; }

        public List<MedicalSummary> MedicalSummaries { get; set; }
        public List<CertificationSummary> CertificationSummaries { get; set; }
        public List<CPRSummary> CPRSummaries { get; set; }

        public List<Training.TrainingSummaryItem> TrainingSummaries { get; set; }

    }
}
