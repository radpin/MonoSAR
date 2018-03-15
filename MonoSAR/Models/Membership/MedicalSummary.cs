﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class MedicalSummary: AccessorySummaryCore
    {

        public MedicalSummary(Models.DB.MemberMedical dataItem)
        {
            this.Title = dataItem.Medical.Title;
            this.Expiration = dataItem.Expiration;
            this.Issued = dataItem.Issued;
            this.RankOrder = dataItem.Medical.RankOrder;
        }

    }
}