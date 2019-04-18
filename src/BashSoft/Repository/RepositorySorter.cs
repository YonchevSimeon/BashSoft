namespace BashSoft.Repository
{
    using System.Linq;
    using System.Collections.Generic;

    using BashSoft.IO;
    using BashSoft.Contracts;
    using BashSoft.StaticData;

    public class RepositorySorter : IDataSorter
    {
        public void OrderAndTake(Dictionary<string, double> studentMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if(comparison == "ascending")
            {
                this.PrintStudents(studentMarks
                    .OrderBy(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if(comparison == "descending")
            {
                this.PrintStudents(studentMarks
                    .OrderByDescending(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> studentMarks)
        {
            foreach (KeyValuePair<string, double> student in studentMarks)
            {
                OutputWriter.PrintStudent(student);
            }
        }
    }
}
