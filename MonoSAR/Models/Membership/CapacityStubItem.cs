using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class CapacityStubItem: Training.StumpCore
    {
        public CapacityStubItem(Models.DB.Capacity dataItem)
        {
            this.ID = dataItem.CapacityId;
            this.Title = dataItem.CapacityName;
        }

    }
}
