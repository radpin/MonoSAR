using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class OperationMember
    {
        public int OperationMemberId { get; set; }
        public int OperationId { get; set; }
        public int MemberId { get; set; }
        public DateTime Created { get; set; }

        public Member Member { get; set; }
        public Operation Operation { get; set; }
    }
}
