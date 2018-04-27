using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class Callout
    {
        public int CalloutId { get; set; }
        public string CalloutMessage { get; set; }
        public DateTime Created { get; set; }
    }
}
