using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class MedicalSummaryItem: AccessorySummaryCore
    {

        public MedicalSummaryItem(Models.DB.MemberMedical dataItem)
        {
            this.ID = dataItem.MemberMedicalId;
            this.Title = dataItem.Medical.Title;
            this.Expiration = dataItem.Expiration;
            this.Issued = dataItem.Issued;
            this.RankOrder = dataItem.Medical.RankOrder;
            this.MemberNameLast = dataItem.Member.LastName;
            this.MemberNameFirst = dataItem.Member.FirstName;
            this.MemberID = dataItem.MemberId;
        }

    }
}
