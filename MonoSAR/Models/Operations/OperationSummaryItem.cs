using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Operations
{
    public class OperationSummaryItem
    {
        public Int32 ID;
        public string OperationNumber;
        public string SequenceNumber;
        public DateTime Start;
        public DateTime End;
        public string Title;
        public string Notes;
        public DateTime Created;

        private Models.ApplicationSettings _applicationSettings;
        private DB.monosarsqlContext _context;

        public OperationSummaryItem(Models.DB.Operation dataEntity, Microsoft.Extensions.Options.IOptions<ApplicationSettings> settings, IConfiguration config)
        {
            _applicationSettings = settings.Value;
            this._context = new DB.monosarsqlContext(config);

            this.ID = dataEntity.OperationId;
            this.OperationNumber = dataEntity.OperationNumber;
            this.SequenceNumber = dataEntity.SequenceNumber;
            this.Start = dataEntity.OperationStart;
            this.End = dataEntity.OperationEnd;
            this.Title = dataEntity.Title;
            this.Notes = dataEntity.Notes;
            this.Created = dataEntity.Created;
        }

    }
}
