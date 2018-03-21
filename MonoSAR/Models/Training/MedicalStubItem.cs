using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class MedicalStubItem: StumpCore
    {

        public MedicalStubItem()
        { }

        public MedicalStubItem(Models.DB.Medical dataItem)
        {
            this.ID = dataItem.MedicalId;
            this.Title = dataItem.Title;
            this.Rank = dataItem.RankOrder;
        }

    }
}
