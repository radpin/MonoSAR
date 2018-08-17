using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Operations
{
    public class OperationListItem
    {
        public Int32 ID;
        public string OperationNumber;
        public string SequenceNumber;
        public DateTime Start;
        public DateTime End;
        public string Title;
        public int NumParticipants;

        public OperationListItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        /* Keeping for now (8/16/2018) - Can delete if we don't end up needing this
        public OperationListItem(Models.DB.Operation dataEntity)
        {
            this.ID = dataEntity.OperationId;
            this.OperationNumber = dataEntity.OperationNumber;
            this.SequenceNumber = dataEntity.SequenceNumber;
            this.Start = dataEntity.OperationStart;
            this.End = dataEntity.OperationEnd;
            this.Title = dataEntity.Title;
        }
        */
    }
}
