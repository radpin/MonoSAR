using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingClassSummaryItem
    {
        public Int32 ID { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public List<Training.TrainingClassParticipantSummaryItem> Students { get; set; }

        public List<Training.TrainingClassParticipantSummaryItem> Instructors { get; set; }

        public TrainingClassSummaryItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingClassSummaryItem(Models.DB.TrainingClass dataEntity)
        {
            this.ID = dataEntity.TrainingClassId;
            this.Date = dataEntity.TrainingDate;
            this.Title = dataEntity.Training.TrainingTitle;
            buildStudents(dataEntity);
            buildInstructors(dataEntity);
        }

        private void buildStudents(Models.DB.TrainingClass dataEntity)
        {
            List<Training.TrainingClassParticipantSummaryItem> students = new List<Training.TrainingClassParticipantSummaryItem>();

            foreach (var item in dataEntity.TrainingClassStudent)
            {
                students.Add(new Training.TrainingClassParticipantSummaryItem(item));
            }

            this.Students = students;
        }

        private void buildInstructors(Models.DB.TrainingClass dataEntity)
        {
            List<Training.TrainingClassParticipantSummaryItem> instructors = new List<Training.TrainingClassParticipantSummaryItem>();

            foreach (var item in dataEntity.TrainingClassInstructor)
            {
                instructors.Add(new Training.TrainingClassParticipantSummaryItem(item));
            }

            this.Instructors = instructors;
        }

    }
}
