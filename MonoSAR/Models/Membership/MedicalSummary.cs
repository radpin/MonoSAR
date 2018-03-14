using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class MedicalSummary
    {

        public MedicalSummary(Models.DB.Medical dataItem)
        {
            this.Title = dataItem.Title;
            this.Expiration = dataItem.Expiration;
            this.Issued = dataItem.Issued;
        }

        public String Title { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }

        public Boolean IsExpired
        {
            get
            {
                if (Expiration > DateTime.Now)
                { return true; }

                return false;
            }
        }
    }
}
