namespace BashSoft.Exceptions
{
    using System;

    public class InvalidTakeQuantityParameterException : Exception
    {
        private const string InvalidTakeQuantityParameter = "The take command expected does not match the format wanted!";

        public InvalidTakeQuantityParameterException()
            : base(InvalidTakeQuantityParameter) { }

        public InvalidTakeQuantityParameterException(string message)
            : base(message) { }
    }
}
