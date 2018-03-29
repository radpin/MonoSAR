using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class TrainingMemberInstructor
    {
        public int TrainingMemberInstructorId { get; set; }
        public int TrainingMemberId { get; set; }
        public int IntstructorMemberId { get; set; }
        public int InstructorHours { get; set; }
        public DateTime Created { get; set; }

        public Member IntstructorMember { get; set; }
        public TrainingMember TrainingMember { get; set; }
    }
}
