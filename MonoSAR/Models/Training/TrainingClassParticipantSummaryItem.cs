using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingClassParticipantSummaryItem
    {

        public TrainingClassParticipantSummaryItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingClassParticipantSummaryItem(Models.DB.TrainingClassStudent item)
        {
            this.Hours = item.TrainingClassStudentHours;
            this.MemberName = item.TrainingClassStudentMember.LastName + ", " + item.TrainingClassStudentMember.FirstName;
            this.MemberID = item.TrainingClassStudentMemberId;
        }

        public TrainingClassParticipantSummaryItem(Models.DB.TrainingClassInstructor item)
        {
            this.Hours = item.TrainingClassStudentHours;
            this.MemberName = item.TrainingClassInstructorMember.LastName + ", " + item.TrainingClassInstructorMember.FirstName;
            this.MemberID = item.TrainingClassInstructorMemberId;
        }

        public Decimal Hours { get; set; }
        public String MemberName { get; set; }
        public Int32 MemberID { get; set; }
    }
}
