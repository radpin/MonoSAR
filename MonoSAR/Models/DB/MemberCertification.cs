using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class MemberCertification
    {
        public int MemberCertificationId { get; set; }
        public int MemberId { get; set; }
        public int CertificationId { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }

        public Certification Certification { get; set; }
        public Member Member { get; set; }
    }
}
