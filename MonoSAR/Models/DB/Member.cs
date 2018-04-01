using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class Member
    {
        public Member()
        {
            MemberCertification = new HashSet<MemberCertification>();
            MemberCpr = new HashSet<MemberCpr>();
            MemberMedical = new HashSet<MemberMedical>();
            OperationMember = new HashSet<OperationMember>();
            TrainingClassInstructor = new HashSet<TrainingClassInstructor>();
            TrainingClassStudent = new HashSet<TrainingClassStudent>();
            TrainingMember = new HashSet<TrainingMember>();
            TrainingMemberInstructor = new HashSet<TrainingMemberInstructor>();
        }

        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ham { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Email { get; set; }
        public DateTime Joined { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneCell { get; set; }
        public string Status { get; set; }
        public int? CapacityId { get; set; }

        public Capacity Capacity { get; set; }
        public ICollection<MemberCertification> MemberCertification { get; set; }
        public ICollection<MemberCpr> MemberCpr { get; set; }
        public ICollection<MemberMedical> MemberMedical { get; set; }
        public ICollection<OperationMember> OperationMember { get; set; }
        public ICollection<TrainingClassInstructor> TrainingClassInstructor { get; set; }
        public ICollection<TrainingClassStudent> TrainingClassStudent { get; set; }
        public ICollection<TrainingMember> TrainingMember { get; set; }
        public ICollection<TrainingMemberInstructor> TrainingMemberInstructor { get; set; }
    }
}
