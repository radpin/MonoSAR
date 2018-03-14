using System;
using System.Collections.Generic;

namespace MonoSAR.Modelx
{
    public partial class Training
    {
        public Training()
        {
            TrainingMember = new HashSet<TrainingMember>();
        }

        public int TrainingId { get; set; }
        public string TrainingTitle { get; set; }

        public ICollection<TrainingMember> TrainingMember { get; set; }
    }
}
