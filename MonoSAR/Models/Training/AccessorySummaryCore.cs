using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public abstract class AccessorySummaryCore
    {
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }
        public Int32 RankOrder { get; set; }

        public String IssuedPretty { get { return Issued.ToShortDateString(); } }
        public String ExpirationPretty { get { return Expiration.ToShortDateString(); } }

        public String MemberNameLast { get; set; }
        public String MemberNameFirst { get; set; }

        public Int32 MemberID { get; set; }

        public Boolean IsExpired
        {
            get
            {
                if (Expiration > DateTime.UtcNow)
                { return true; }

                return false;
            }
        }
    }
}
