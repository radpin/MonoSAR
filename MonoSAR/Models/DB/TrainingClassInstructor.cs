using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class TrainingClassInstructor
    {
        public int TrainingClassInstructorId { get; set; }
        public int TrainingClassInstructorMemberId { get; set; }
        public int TrainingClassId { get; set; }
        public decimal TrainingClassStudentHours { get; set; }
        public DateTime Created { get; set; }

        public TrainingClass TrainingClass { get; set; }
        public Member TrainingClassInstructorMember { get; set; }
    }
}
