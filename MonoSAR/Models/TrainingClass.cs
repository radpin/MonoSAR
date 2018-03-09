using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models
{
    public class TrainingClass
    {
        public DB.Training Training { get; set; }
        public ICollection<DB.Member> Members { get; set; }
    }
}
