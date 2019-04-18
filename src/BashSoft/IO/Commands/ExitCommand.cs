namespace BashSoft.IO.Commands
{
    using System;
    using BashSoft.Attributes;

    [Alias("exit")]
    public class ExitCommand : Command
    {
        public ExitCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}
