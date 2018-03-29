using System;
using System.Collections.Generic;

namespace MonoSAR.Modeldump
{
    public partial class Capacity
    {
        public Capacity()
        {
            Member = new HashSet<Member>();
        }

        public int CapacityId { get; set; }
        public string CapacityName { get; set; }

        public ICollection<Member> Member { get; set; }
    }
}
