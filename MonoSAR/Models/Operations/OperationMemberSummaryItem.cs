using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Operations
{
    public class OperationMemberSummaryItem
    {
        public Int32 OperationMemberID { get; set; }
        public Int32 OperationID { get; set; }
        public Int32 MemberID { get; set; }
        public String MemberName { get; set; }

        public OperationMemberSummaryItem(Models.DB.OperationMember dataItem)
        {
            this.OperationMemberID = dataItem.OperationMemberId;
            this.OperationID = dataItem.OperationId;
            this.MemberID = dataItem.MemberId;
            this.MemberName = dataItem.Member.LastName + ", " + dataItem.Member.FirstName;
        }

    }
}
