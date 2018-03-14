using System;
using System.Collections.Generic;

namespace MonoSAR.Modelx
{
    public partial class Medical
    {
        public Medical()
        {
            MemberMedical = new HashSet<MemberMedical>();
        }

        public int MedicalId { get; set; }
        public string Title { get; set; }
        public int RankOrder { get; set; }
        public DateTime Created { get; set; }

        public ICollection<MemberMedical> MemberMedical { get; set; }
    }
}
