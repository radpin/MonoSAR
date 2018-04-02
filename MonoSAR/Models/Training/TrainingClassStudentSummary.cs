using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingClassStudentSummary: List<TrainingClassStudentSummaryItem>
    {
        public TrainingClassStudentSummary(IEnumerable<Models.DB.TrainingClassStudent> datalist)
        {
            foreach (var dataItem in datalist)
            { this.Add(new TrainingClassStudentSummaryItem(dataItem)); }
        }
    }
}
