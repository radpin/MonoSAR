using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingClassInstructorSummaryItem
    {

        public TrainingClassInstructorSummaryItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingClassInstructorSummaryItem(Models.DB.TrainingClassInstructor dataItem)
        {
            //just in case

            if (dataItem == null)
            { throw new Exception("TrainingClassInstructor not found."); }

            if (dataItem.TrainingClass == null)
            { throw new Exception("Training class not attached, perhaps not eagerly loaded?"); }

            if (dataItem.TrainingClass.Training == null)
            { throw new Exception("Training not attached, perhaps not eagerly loaded?"); }

            if (dataItem.TrainingClassInstructorMember == null)
            { throw new Exception("Instructor member not attached, perhaps not eagerly loaded?"); }


            this.Created = dataItem.Created;
            this.Hours = dataItem.TrainingClassStudentHours;
            this.MemberName = dataItem.TrainingClassInstructorMember.LastName + ", " + dataItem.TrainingClassInstructorMember.FirstName;
            this.MemberID = dataItem.TrainingClassInstructorMemberId;
            this.TrainingTitle = dataItem.TrainingClass.Training.TrainingTitle;
            this.TrainingDate = dataItem.TrainingClass.TrainingDate;
            this.TrainingClassInstructorID = dataItem.TrainingClassInstructorId;
        }


        public String TrainingTitle { get; set; }
        public DateTime TrainingDate { get; set; }
        public String TrainingDatePretty { get { return TrainingDate.ToShortDateString(); } }
        public DateTime Created { get; set; }
        public String CreatedPretty { get { return Created.ToShortDateString(); } }
        public Decimal Hours { get; set; }
        public String MemberName { get; set; }
        public Int32 TrainingClassInstructorID { get; set; }
        public Int32 MemberID { get; set; }
    }
}
