using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class CertificationSummary: AccessorySummaryCore
    {
        public CertificationSummary(Models.DB.MemberCertification dataItem)
        {
            this.Title = dataItem.Certification.Title;
            this.Expiration = dataItem.Expiration;
            this.Issued = dataItem.Issued;
            this.RankOrder = dataItem.Certification.RankOrder;
        }
    }
}
