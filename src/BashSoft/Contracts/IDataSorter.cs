﻿namespace BashSoft.Contracts
{
    using System.Collections.Generic;

    public interface IDataSorter
    {
        void OrderAndTake(Dictionary<string, double> studentMarks, string comparison, int studentsToTake);
    }
}
