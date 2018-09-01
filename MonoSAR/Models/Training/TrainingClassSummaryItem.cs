using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models.Training
{
    public class TrainingClassSummaryItem
    {
        public Int32 TrainingClassID { get; set; }

        public DateTime TrainingDate { get; set; }

        public string TrainingTitle { get; set; }

        public Int32 TrainingID { get; set; }

        public List<Training.TrainingClassStudentSummaryItem> Students { get; set; }

        public List<Training.TrainingClassInstructorSummaryItem> Instructors { get; set; }

        public TrainingClassSummaryItem()
        {
            //parameterless constructors are required for mvc binding, this should not be used for user created code
        }

        public TrainingClassSummaryItem(Models.DB.TrainingClass dataEntity)
        {
            this.TrainingClassID = dataEntity.TrainingClassId;
            this.TrainingDate = dataEntity.TrainingDate;
            this.TrainingTitle = dataEntity.Training.TrainingTitle;
            this.TrainingID = dataEntity.TrainingId;
            buildStudents(dataEntity);
            buildInstructors(dataEntity);
        }

        private void buildStudents(Models.DB.TrainingClass dataEntity)
        {
            List<Training.TrainingClassStudentSummaryItem> students = new List<Training.TrainingClassStudentSummaryItem>();

            foreach (var item in dataEntity.TrainingClassStudent)
            {
                students.Add(new Training.TrainingClassStudentSummaryItem(item));
            }

            this.Students = students;
        }

        private void buildInstructors(Models.DB.TrainingClass dataEntity)
        {
            List<Training.TrainingClassInstructorSummaryItem> instructors = new List<Training.TrainingClassInstructorSummaryItem>();

            foreach (var item in dataEntity.TrainingClassInstructor)
            {
                instructors.Add(new Training.TrainingClassInstructorSummaryItem(item));
            }

            this.Instructors = instructors;
        }

    }
}
