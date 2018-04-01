using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class Training
    {
        public Training()
        {
            TrainingClass = new HashSet<TrainingClass>();
            TrainingMember = new HashSet<TrainingMember>();
        }

        public int TrainingId { get; set; }
        public string TrainingTitle { get; set; }

        public ICollection<TrainingClass> TrainingClass { get; set; }
        public ICollection<TrainingMember> TrainingMember { get; set; }
    }
}
