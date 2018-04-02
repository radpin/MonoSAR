using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class TrainingClassStudent
    {
        public int TrainingClassStudentId { get; set; }
        public int TrainingClassStudentMemberId { get; set; }
        public int TrainingClassId { get; set; }
        public decimal TrainingClassStudentHours { get; set; }
        public DateTime Created { get; set; }

        public TrainingClass TrainingClass { get; set; }
        public Member TrainingClassStudentMember { get; set; }
    }
}
