using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Operations
{
    public class OperationUpdate
    {
        public Int32 ID { get; set; }
        public string OperationNumber { get; set; }
        public string SequenceNumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }

        public OperationUpdate()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public OperationUpdate(Models.DB.Operation dataEntity)
        {
            this.ID = dataEntity.OperationId;
            this.OperationNumber = dataEntity.OperationNumber;
            this.SequenceNumber = dataEntity.SequenceNumber;
            this.Start = dataEntity.OperationStart;
            this.End = dataEntity.OperationEnd;
            this.Title = dataEntity.Title;
            this.Notes = dataEntity.Notes;
        }


    }
}
