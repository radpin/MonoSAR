using System;
using System.Collections.Generic;

namespace MonoSAR.Modeldump
{
    public partial class Cpr
    {
        public Cpr()
        {
            MemberCpr = new HashSet<MemberCpr>();
        }

        public int Cprid { get; set; }
        public string Title { get; set; }
        public int RankOrder { get; set; }
        public DateTime Created { get; set; }

        public ICollection<MemberCpr> MemberCpr { get; set; }
    }
}
