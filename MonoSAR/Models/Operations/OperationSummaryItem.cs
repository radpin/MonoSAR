using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Operations
{
    public class OperationSummaryItem
    {
        public Int32 ID { get; set; }
        public string OperationNumber { get; set; }
        public string SequenceNumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public List<Membership.MemberStubItem> MemberStubs { get; set; }

        public OperationSummaryItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public OperationSummaryItem(Models.DB.Operation dataEntity)
        {
            this.ID = dataEntity.OperationId;
            this.OperationNumber = dataEntity.OperationNumber;
            this.SequenceNumber = dataEntity.SequenceNumber;
            this.Start = dataEntity.OperationStart;
            this.End = dataEntity.OperationEnd;
            this.Title = dataEntity.Title;
            this.Notes = dataEntity.Notes;
            buildMemberStubs(dataEntity);
        }


        private void buildMemberStubs(Models.DB.Operation dataEntity)
        {
            List<Membership.MemberStubItem> memberStubs = new List<Membership.MemberStubItem>();

            foreach (var item in dataEntity.OperationMember)
            {
                memberStubs.Add(new Membership.MemberStubItem(item.Member));
            }

            this.MemberStubs = memberStubs;
        }
    }
}
