using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public abstract class StumpCore
    {        
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public Int32 Rank { get; set; }
    }
}
