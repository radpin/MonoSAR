using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingInsert
    {
        public string Title { get; set; }

        public TrainingInsert()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingInsert(Models.DB.Training dataEntity)
        {
            this.Title = dataEntity.TrainingTitle;
        }
    }
}
