using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class CPRSummaryItem: AccessorySummaryCore
    {

        /// <summary>
        /// Just for MVC binding. Model bound complex types must not be abstract or value types and must have a parameterless constructor.
        /// </summary>
        public CPRSummaryItem()
        { }

        public CPRSummaryItem(Models.DB.MemberCpr dataItem)
        {
            this.ID = dataItem.MemberCprid;
            this.Title = dataItem.Cpr.Title;
            this.Expiration = dataItem.Expiration;
            this.Issued = dataItem.Issued;
            this.RankOrder = dataItem.Cpr.RankOrder;
            this.MemberNameLast = dataItem.Member.LastName;
            this.MemberNameFirst = dataItem.Member.FirstName;
            this.MemberID = dataItem.MemberId;
        }
    }
}
