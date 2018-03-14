using System;
using System.Collections.Generic;

namespace MonoSAR.Modelx
{
    public partial class MemberMedical
    {
        public int MemberMedicalId { get; set; }
        public int MemberId { get; set; }
        public int MedicalId { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }

        public Medical Medical { get; set; }
        public Member Member { get; set; }
    }
}
