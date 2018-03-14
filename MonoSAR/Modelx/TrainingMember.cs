using System;
using System.Collections.Generic;

namespace MonoSAR.Modelx
{
    public partial class TrainingMember
    {
        public int TrainingMemberId { get; set; }
        public int TrainingId { get; set; }
        public int MemberId { get; set; }
        public decimal TrainingHours { get; set; }
        public DateTime TrainingDate { get; set; }
        public DateTime Created { get; set; }

        public Member Member { get; set; }
        public Training Training { get; set; }
    }
}
