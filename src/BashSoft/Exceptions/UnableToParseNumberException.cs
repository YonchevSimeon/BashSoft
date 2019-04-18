namespace BashSoft.Exceptions
{
    using System;

    public class UnableToParseNumberException : Exception
    {
        public const string UnableToParseNumber = "The seqeuence you've written is not a valid number.";

        public UnableToParseNumberException()
            : base(UnableToParseNumber) { }

        public UnableToParseNumberException(string message)
            :base(message) { }
    }
}
