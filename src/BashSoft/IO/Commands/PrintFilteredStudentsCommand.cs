﻿namespace BashSoft.IO.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Attributes;
    using BashSoft.Exceptions;

    [Alias("filter")]
    public class PrintFilteredStudentsCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public PrintFilteredStudentsCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            if(this.Data.Length != 5)
            {
                throw new InvalidCommandException(this.Input);
            }
            string courseName = this.Data[1];
            string filter = this.Data[2];
            string takeCommand = this.Data[3].ToLower();
            string takeQuantity = this.Data[4].ToLower();

            this.TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        }

        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        throw new InvalidTakeQuantityParameterException();
                    }
                }
            }
            else
            {
                throw new InvalidTakeQuantityParameterException();
            }
        }
    }
}
