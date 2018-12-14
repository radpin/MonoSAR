using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingClassStudentSummaryItem
    {

        public TrainingClassStudentSummaryItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingClassStudentSummaryItem(Models.DB.TrainingClassStudent dataItem)
        {
            //just in case

            if (dataItem == null)
            { throw new Exception("TrainingClassStudent not found."); }

            if (dataItem.TrainingClass == null)
            { throw new Exception("Training class not attached, perhaps not eagerly loaded?"); }

            if (dataItem.TrainingClass.Training == null)
            { throw new Exception("Training not attached, perhaps not eagerly loaded?"); }

            if (dataItem.TrainingClassStudentMember == null)
            { throw new Exception("Student member not attached, perhaps not eagerly loaded?"); }


            this.Created = dataItem.Created;
            this.Hours = dataItem.TrainingClassStudentHours;
            this.MemberName = dataItem.TrainingClassStudentMember.LastName + ", " + dataItem.TrainingClassStudentMember.FirstName;
            this.MemberID = dataItem.TrainingClassStudentMemberId;
            this.TrainingTitle = dataItem.TrainingClass.Training.TrainingTitle;
            this.TrainingDate = dataItem.TrainingClass.TrainingDate;
            this.TrainingClassStudentID = dataItem.TrainingClassStudentId;
            this.TrainingClassID = dataItem.TrainingClassId;
        }


        public String TrainingTitle { get; set; }
        public DateTime TrainingDate { get; set; }
        public String TrainingDatePretty { get { return TrainingDate.ToShortDateString(); } }
        public DateTime Created { get; set; }
        public String CreatedPretty { get { return Created.ToShortDateString(); } }
        public Decimal Hours { get; set; }
        public String MemberName { get; set; }
        public Int32 TrainingClassStudentID { get; set; }
        public Int32 TrainingClassID { get; set; }
        public Int32 MemberID { get; set; }
    }
}
