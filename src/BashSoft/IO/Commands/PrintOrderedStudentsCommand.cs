﻿namespace BashSoft.IO.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Attributes;
    using BashSoft.Exceptions;

    [Alias("order")]
    public class PrintOrderedStudentsCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public PrintOrderedStudentsCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            if(this.Data.Length != 5)
            {
                throw new InvalidCommandException(this.Input);
            }
            string courseName = this.Data[1];
            string orderType = this.Data[2].ToLower();
            string orderCommand = this.Data[3].ToLower();
            string orderQuantity = this.Data[4].ToLower();

            this.TryParseParametersForOrderAndTake(orderCommand, orderQuantity, courseName, orderType);
        }

        private void TryParseParametersForOrderAndTake(string orderCommand, string orderQuantity, string courseName, string orderType)
        {
            if (orderCommand == "take")
            {
                if (orderQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, orderType);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(orderQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, orderType, studentsToTake);
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
