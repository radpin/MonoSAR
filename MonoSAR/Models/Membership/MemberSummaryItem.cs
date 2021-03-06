﻿using Microsoft.Extensions.Configuration;
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
        private DB.monosarsqlContext _context;

        public MemberSummaryItem(Models.DB.Member dataEntity, Microsoft.Extensions.Options.IOptions<ApplicationSettings> settings, IConfiguration config)
        {
            _applicationSettings = settings.Value;
            this._context = new DB.monosarsqlContext(config);
            this.ID = dataEntity.MemberId;
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
            this.buildTraining(dataEntity);

            this.buildOperations(dataEntity);

            this.Capacity = dataEntity.Capacity.CapacityName;
        }
        
        private void buildMedical(Models.DB.Member dataItem)
        {
            List<Training.MedicalSummaryItem> medicalSummaries = new List<Training.MedicalSummaryItem>();

            foreach (var item in dataItem.MemberMedical)
            {
                medicalSummaries.Add(new Training.MedicalSummaryItem(item));
            }

            this.MedicalSummaries = medicalSummaries;

            if (dataItem.MemberMedical.Count > 0)
            { this.MedicalExpires = dataItem.MemberMedical.OrderByDescending(e => e.Expiration).FirstOrDefault().Expiration; }
        }

        private void buildCPR(Models.DB.Member dataItem)
        {
            List<Training.CPRSummaryItem> cPRSummaries = new List<Training.CPRSummaryItem>();

            foreach (var item in dataItem.MemberCpr)
            {
                cPRSummaries.Add(new Training.CPRSummaryItem(item));
            }

            this.CPRSummaries = cPRSummaries;

            //this one
            if (dataItem.MemberCpr.Count > 0)
            { this.CPRExpires = dataItem.MemberCpr.OrderByDescending( e => e.Expiration).FirstOrDefault().Expiration; }

        }

        private void buildCertification(Models.DB.Member dataItem)
        {
            List<Training.CertificationSummary> certificationSummaries = new List<Training.CertificationSummary>();

            foreach (var item in dataItem.MemberCertification)
            {
                certificationSummaries.Add(new Training.CertificationSummary(item));
            }

            this.CertificationSummaries = certificationSummaries;
        }

        private void buildInitialFieldChecks(Models.DB.Member dataItem)
        {
            var bv = (from tm in dataItem.TrainingClassStudent
                     where tm.TrainingClassStudentMemberId == dataItem.MemberId && tm.TrainingClass.TrainingId == _applicationSettings.RequiredBVTest
                     select tm).FirstOrDefault();

            var bcc = (from tm in dataItem.TrainingClassStudent
                       where tm.TrainingClassStudentMemberId == dataItem.MemberId && tm.TrainingClass.TrainingId == _applicationSettings.RequiredCandidateBasicClass
                          select tm).FirstOrDefault();

            var ics100 = (from tm in dataItem.TrainingClassStudent
                          where tm.TrainingClassStudentMemberId == dataItem.MemberId && tm.TrainingClass.TrainingId == _applicationSettings.RequiredICS100
                       select tm).FirstOrDefault();

            var ics200 = (from tm in dataItem.TrainingClassStudent
                          where tm.TrainingClassStudentMemberId == dataItem.MemberId && tm.TrainingClass.TrainingId == _applicationSettings.RequiredICS200
                          select tm).FirstOrDefault();

            var pc = (from tm in dataItem.TrainingClassStudent
                      where tm.TrainingClassStudentMemberId == dataItem.MemberId && tm.TrainingClass.TrainingId == _applicationSettings.RequiredPackCheck
                      select tm).FirstOrDefault();

            if (bv != null) { this.IsBuildingVehicleTested = true; }
            if (bcc != null) { this.IsCandidateClass = true; }
            if (ics100 != null) { this.IsICS100 = true; }
            if (ics200 != null) { this.IsICS200 = true; }
            if (pc != null) { this.IsPackChecked = true; }
        }

        private void buildBeaconCheck(Models.DB.Member dataItem)
        {
            var beac = (from tm in dataItem.TrainingClassStudent
                        where tm.TrainingClassStudentMemberId == dataItem.MemberId 
                        && tm.TrainingClass.TrainingId == _applicationSettings.RequiredBeaconTest
                        orderby tm.Created descending
                        select tm).FirstOrDefault();

            this.IsBeaconExpired = true;

            if (beac != null && beac.TrainingClass.TrainingDate > DateTime.UtcNow.AddYears(-1) )
            {
                this.IsBeaconExpired = false;
                this.BeaconExpires = beac.TrainingClass.TrainingDate.AddYears(1);
            }
           
        }

        private void buildTraining(Models.DB.Member dataItem)
        {

            List<Training.TrainingClassStudentSummaryItem> trainingSummaries = new List<Training.TrainingClassStudentSummaryItem>();
            this.TrainingSummaries = trainingSummaries;

            foreach (var tm in dataItem.TrainingClassStudent)
            {
                Training.TrainingClassStudentSummaryItem tsi = new Training.TrainingClassStudentSummaryItem(tm);
                trainingSummaries.Add(tsi);
            }

            if (dataItem.TrainingClassStudent.Count > 0)
            {
                this.TrainingSummaries = trainingSummaries.OrderByDescending(x => x.TrainingDate).ToList();
            }


        }

        private void buildOperations(Models.DB.Member dataItem)
        {
            this.OperationSummaries = dataItem.OperationMember.Select(o => new Operations.OperationSummaryItem(o.Operation)).ToList();
            this.OperationSummaries = OperationSummaries.OrderByDescending(x => x.Start).ToList();
        }

        public Int32 ID { get; set; }
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
                if (MedicalExpires > DateTime.UtcNow)
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
                if (CPRExpires > DateTime.UtcNow)
                { return false; }

                return true;
            }
        }
        public Boolean IsBeaconExpired{get;set;}
        public DateTime BeaconExpires { get; set; }
        public String BeaconExpiresPretty { get { return BeaconExpires.ToShortDateString(); } }
        public Boolean IsWinterFieldReady
        {
            get
            {
                if (!IsMedicalExpired && !IsCPRExpired && !IsBeaconExpired && IsCandidateClass && IsICS100 && IsICS200 && IsBuildingVehicleTested && IsPackChecked && IsCandidateClass && (Capacity.ToLower() == "candidate" || Capacity.ToLower() == "rescue" || Capacity.ToLower() == "life" )         )
                { return true; }
                else
                { return false; }
            }
        }
        public Boolean IsSummerFieldReady
        {
            get
            {
                if (!IsMedicalExpired && !IsCPRExpired && IsCandidateClass && IsICS100 && IsICS200 && IsBuildingVehicleTested && IsPackChecked && IsCandidateClass && (Capacity.ToLower() == "candidate" || Capacity.ToLower() == "rescue" || Capacity.ToLower() == "life"))
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

        public List<Training.MedicalSummaryItem> MedicalSummaries { get; set; }
        public List<Training.CertificationSummary> CertificationSummaries { get; set; }
        public List<Training.CPRSummaryItem> CPRSummaries { get; set; }

        public List<Training.TrainingClassStudentSummaryItem> TrainingSummaries { get; set; }

        public List<Operations.OperationSummaryItem> OperationSummaries { get; set; }
    }
}
