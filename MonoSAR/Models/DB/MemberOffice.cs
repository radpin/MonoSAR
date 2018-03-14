using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class MemberOffice
    {
        public int MemberOfficeId { get; set; }
        public int MemberId { get; set; }
        public int OfficeId { get; set; }
        public DateTime Created { get; set; }

        public virtual Office Office { get; set; }
    }
}
