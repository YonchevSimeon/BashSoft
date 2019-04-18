namespace BashSoft.Exceptions
{
    using System;

    public class NegativeCapacityException : Exception
    {
        private const string NegativeMessage = "Capacity cannot be negative!";

        public NegativeCapacityException()
            : base(NegativeMessage) { }

        public NegativeCapacityException(string message)
            : base(message) { }
    }
}
