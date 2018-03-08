using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class Member
    {
        public Member()
        {
            TrainingMember = new HashSet<TrainingMember>();
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

        public ICollection<TrainingMember> TrainingMember { get; set; }
    }
}
