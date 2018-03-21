using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class MedicalStubs: List<MedicalStubItem>
    {
        public MedicalStubs()
        { }

        public MedicalStubs(List<Models.DB.Medical> dataItem)
        {
            foreach (var x in dataItem)
            {
                this.Add(new MedicalStubItem(x));
            }
        }
    }
}
