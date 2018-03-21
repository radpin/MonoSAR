using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class CPRStumpItem: StumpCore
    {
        public CPRStumpItem(Models.DB.Cpr dataItem)
        {
            this.ID = dataItem.Cprid;
            this.Title = dataItem.Title;
            this.Rank = dataItem.RankOrder;
        }
    }
}
