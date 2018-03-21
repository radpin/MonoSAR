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
            this.Title = dataItem.Medical.Title;
            this.Expiration = dataItem.Expiration;
            this.Issued = dataItem.Issued;
            this.RankOrder = dataItem.Medical.RankOrder;
        }

    }
}
