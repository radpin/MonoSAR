using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingSummaryItem
    {

        public TrainingSummaryItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingSummaryItem(Models.DB.TrainingMember dataItem)
        {
            //just in case

            if (dataItem == null)
            { throw new Exception("MemberTraining not found."); }

            if (dataItem.Training == null)
            { throw new Exception("Training not attached to TrainingMember, perhaps not eagerly loaded?"); }

            if (dataItem.Member == null)
            { throw new Exception("Member not attached to TrainingMember, perhaps not eagerly loaded?"); }


            this.Created = dataItem.Created;
            this.Hours = dataItem.TrainingHours;
            this.MemberName = dataItem.Member.LastName + ", " + dataItem.Member.FirstName;
            this.TrainingMemberID = dataItem.TrainingMemberId;
            this.TrainingTitle = dataItem.Training.TrainingTitle;
            this.When = dataItem.TrainingDate;
        }


        public String TrainingTitle { get; set; }
        public DateTime When { get; set; }
        public String WhenPretty { get { return When.ToShortDateString(); } }
        public DateTime Created { get; set; }
        public Decimal Hours { get; set; }
        public String MemberName { get; set; }
        public Int32 TrainingMemberID { get; set; }
    }
}
