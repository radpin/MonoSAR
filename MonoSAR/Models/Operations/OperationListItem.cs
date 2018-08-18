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
    }
}
