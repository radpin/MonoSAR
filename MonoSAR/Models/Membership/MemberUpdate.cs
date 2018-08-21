using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class MemberUpdate
    {
        public Int32 MemberID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String Email { get; set; }
        public String PhoneHome { get; set; }
        public String PhoneWork { get; set; }
        public String PhoneCell { get; set; }
        public CapacityStubs CapacityStubs { get; set; }
        public Int32 CapacityID { get; set; }


        public MemberUpdate()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public MemberUpdate(Models.DB.Member dataEntity)
        {
            this.MemberID = dataEntity.MemberId;
            this.FirstName = dataEntity.FirstName;
            this.LastName = dataEntity.LastName;
            this.Address = dataEntity.Address;
            this.City = dataEntity.City;
            this.State = dataEntity.State;
            this.Zip = dataEntity.Zipcode;
            this.Email = dataEntity.Email;
            this.PhoneHome = dataEntity.PhoneHome;
            this.PhoneWork = dataEntity.PhoneWork;
            this.PhoneCell = dataEntity.PhoneCell;
            this.CapacityID = dataEntity.CapacityId ?? 0;
        }
    }
}
