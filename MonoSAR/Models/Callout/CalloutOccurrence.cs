using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Callout
{
    public class CalloutOccurrence
    {
        public String VoiceMessage { get; set; }
        public String Location { get; set; }
        public Boolean BringRescueVehicles { get; set; }
        public String Opsleader { get; set; }
    }
}
