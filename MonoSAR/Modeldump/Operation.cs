using System;
using System.Collections.Generic;

namespace MonoSAR.Modeldump
{
    public partial class Operation
    {
        public Operation()
        {
            OperationMember = new HashSet<OperationMember>();
        }

        public int OperationId { get; set; }
        public string OperationNumber { get; set; }
        public string SequenceNumber { get; set; }
        public DateTime OperationStart { get; set; }
        public DateTime OperationEnd { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }

        public ICollection<OperationMember> OperationMember { get; set; }
    }
}
