using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class CPRStumps: List<CPRStumpItem>
    {
        public CPRStumps(List<Models.DB.Cpr> datalist)
        {
            foreach (var x in datalist)
            { this.Add(new CPRStumpItem(x)); }
        }
    }
}
