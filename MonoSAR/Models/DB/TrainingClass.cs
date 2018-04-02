using System;
using System.Collections.Generic;

namespace MonoSAR.Models.DB
{
    public partial class TrainingClass
    {
        public TrainingClass()
        {
            TrainingClassInstructor = new HashSet<TrainingClassInstructor>();
            TrainingClassStudent = new HashSet<TrainingClassStudent>();
        }

        public int TrainingClassId { get; set; }
        public int TrainingId { get; set; }
        public DateTime TrainingDate { get; set; }
        public DateTime Created { get; set; }

        public Training Training { get; set; }
        public ICollection<TrainingClassInstructor> TrainingClassInstructor { get; set; }
        public ICollection<TrainingClassStudent> TrainingClassStudent { get; set; }
    }
}
