using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingSummaryItem
    {
        public String TrainingTitle { get; set; }
        public DateTime When { get; set; }
        public DateTime Created { get; set; }
        public Decimal Hours { get; set; }
        public String MemberName { get; set; }
        public Int32 TrainingMemberID { get; set; }
    }
}
