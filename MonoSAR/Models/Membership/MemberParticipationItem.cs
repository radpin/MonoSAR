using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Membership
{
/// <summary>
/// This transforms the EF model of "member", and the child properties and collections, into a model for MVC binding. The child objects (medical, certs, cpr, etc) are 
/// have their transformations as part of their constructors. 
/// </summary>
    public class MemberParticipationItem
    {
        public MemberParticipationItem()
        {
        }

        public Int32 ID { get; set; }
        public String First { get; set; }
        public String Last { get; set; }
        public int NumOperationsThisYear { get; set; }
        public int NumTrainingsThisYear { get; set; }
        public int NumOperationsTotal { get; set; }
        public int NumTrainingsTotal { get; set; }
    }
}
