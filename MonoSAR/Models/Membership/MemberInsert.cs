using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class MemberInsert
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String Email { get; set; }
        public DateTime Joined { get; set; }
        public String PhoneHome { get; set; }
        public String PhoneWork { get; set; }
        public String PhoneCell { get; set; }
        public CapacityStubs CapacityStubs { get; set; }
        public Int32 CapacityID { get; set; }
    }
}
