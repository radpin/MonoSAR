using System;
using System.Collections.Generic;

namespace MonoSAR.Modelx
{
    public partial class Certification
    {
        public Certification()
        {
            MemberCertification = new HashSet<MemberCertification>();
        }

        public int CertificationId { get; set; }
        public string Title { get; set; }
        public int RankOrder { get; set; }
        public DateTime Created { get; set; }

        public ICollection<MemberCertification> MemberCertification { get; set; }
    }
}
