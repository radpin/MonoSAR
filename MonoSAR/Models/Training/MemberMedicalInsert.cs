using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class MemberMedicalInsert
    {

        public MemberMedicalInsert()
        {
            //only used for under-the-hood mvc binding
        }


        public Int32 MedicalID { get; set; }
        public Int32 MemberID { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expiration { get; set; }
        public Models.Membership.MemberStubs MemberStubs { get; set; }
        public Training.MedicalStubs MedicalStubs { get; set; }

    }
}
