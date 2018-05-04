using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class CPRSummary: List<CPRSummaryItem>
    {
        public CPRSummary(IEnumerable<Models.DB.MemberCpr> datalist)
        {
            foreach (var x in datalist)
            {
                base.Add(new CPRSummaryItem(x));
            }
        }
    }
}
