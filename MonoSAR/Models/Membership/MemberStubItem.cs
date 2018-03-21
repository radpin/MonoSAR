using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{ 
    public class MemberStubItem
    {
        public Int32 MemberID { get; set; }
        public String MemberName { get; set; }

        public MemberStubItem(Models.DB.Member dataItem)
        {
            this.MemberID = dataItem.MemberId;
            this.MemberName = dataItem.LastName + ", " + dataItem.FirstName;
        }
    }
}
