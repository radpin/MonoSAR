using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class Office
    {
        public Office()
        {
            MemberOffice = new HashSet<MemberOffice>();
        }

        public int OfficeId { get; set; }
        public string OfficeName { get; set; }

        public ICollection<MemberOffice> MemberOffice { get; set; }
    }
}
