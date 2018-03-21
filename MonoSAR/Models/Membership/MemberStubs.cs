using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class MemberStubs : List<Models.Membership.MemberStubItem>
    {
        public MemberStubs(IEnumerable<Models.DB.Member> dataList)
        {
            foreach (var x in dataList)
            { this.Add(new MemberStubItem(x));  }
        }
    }
}
