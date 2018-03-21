using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class MemberCPRInsert
    {
        public Int32 CPRID { get; set; }
        public Int32 MemberID { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }

        public Models.Membership.MemberStubs MemberStubs { get; set; }

        public Models.Training.CPRStumps CPRStumps { get; set; }
    }
}
