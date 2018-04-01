using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingClassInsert
    {
        public Int32 TrainingID { get; set; }

        public DateTime TrainingDate { get; set; }

        public IEnumerable<Training.TrainingClassParticipant> Participants { get; set; }

        public IEnumerable<Training.TrainingClassParticipant> Instructors { get; set; }

    }
}
