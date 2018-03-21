using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class CPRSummary: AccessorySummaryCore
    {
        public CPRSummary(Models.DB.MemberCpr dataItem)
        {
            this.Title = dataItem.Cpr.Title;
            this.Expiration = dataItem.Expiration;
            this.Issued = dataItem.Issued;
            this.RankOrder = dataItem.Cpr.RankOrder;
        }
    }
}
