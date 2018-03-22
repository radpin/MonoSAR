using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class CapacityStubs: List<CapacityStubItem>
    {
        public CapacityStubs()
        { }

        public CapacityStubs(List<Models.DB.Capacity> list)
        {
            foreach (var x in list)
            {
                this.Add(new CapacityStubItem(x));
            }
        }
    }
}
