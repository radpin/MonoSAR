using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
    public class MemberSummary: List<MemberSummaryItem>
    {
        public String EmailList
        {
            get
            {
                //this should probably be moved to a method that gets called, rather than having a loop in an accessor.
                //just not worried because of the small list of items

                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

                foreach (var x in this)
                {
                    stringBuilder.Append(x.Email);
                    stringBuilder.Append(" ; ");
                }

                return stringBuilder.ToString();
            }
        }

    }
}
