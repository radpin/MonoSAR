using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingListItem
    {
        public Int32 ID { get; set; }

        public string Title { get; set; }

        public TrainingListItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingListItem(Models.DB.Training dataEntity)
        {
            this.ID = dataEntity.TrainingId;
            this.Title = dataEntity.TrainingTitle;
        }
    }
}
