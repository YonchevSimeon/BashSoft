namespace BashSoft.IO
{
    using System;

    using BashSoft.Contracts;
    using BashSoft.StaticData;

    public class InputReader : IReader
    {
        private const string endCommand = "quit";

        private IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void StartReadingCommands()
        {
            while (true)
            {
                //Interpret command
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                string input = Console.ReadLine();
                input = input.Trim();
                if(input == endCommand)
                {
                    break;
                }
                this.interpreter.InterpretCommand(input);
            }
        }
    }
}
