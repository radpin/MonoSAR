using System;
using System.Collections.Generic;

namespace MonoSAR.Modeldump
{
    public partial class MemberCpr
    {
        public int MemberCprid { get; set; }
        public int MemberId { get; set; }
        public int Cprid { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }

        public Cpr Cpr { get; set; }
        public Member Member { get; set; }
    }
}
