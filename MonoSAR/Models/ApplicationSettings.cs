using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models
{
    public class ApplicationSettings
    {
        public String HomeImagePath { get; set; }
        public Int32 RequiredBVTest { get; set; }
        public Int32 RequiredCandidateBasicClass { get; set; }
        public Int32 RequiredICS100 { get; set; }
        public Int32 RequiredICS200 { get; set; }
        public Int32 RequiredPackCheck { get; set; }
        public Int32 RequiredBeaconTest { get; set; }

    }
}
