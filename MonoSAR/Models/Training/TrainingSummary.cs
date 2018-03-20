using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingSummary: List<TrainingSummaryItem>
    {
        public TrainingSummary(IEnumerable<Models.DB.TrainingMember> datalist)
        {
            foreach (var dataItem in datalist)
            { this.Add(new TrainingSummaryItem(dataItem)); }
        }
    }
}
