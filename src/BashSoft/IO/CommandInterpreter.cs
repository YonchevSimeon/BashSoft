namespace BashSoft.IO
{
    using System;
    using System.Linq;
    using System.Reflection;

    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.IO.Commands;

    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpretCommand(string input)
        {
            string[] data = input.Split(' ');
            string commandName = data[0].ToLower();
            try
            {
                IExecutable command = this.ParseCommand(input, data, commandName);
                command.Execute();
            }
            catch(Exception e)
            {
                OutputWriter.WriteMessageOnNewLine(e.Message);
            }
        }

        private IExecutable ParseCommand(string input, string[] data, string command)
        {
            object[] parametersForConstructor = new object[] { input, data };

            Type typeOfCommand = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .First(type => type
                    .GetCustomAttributes(typeof(AliasAttribute))
                    .Where(atr => atr.Equals(command))
                    .ToArray().Length > 0);

            Type typeOfInterpreter = typeof(CommandInterpreter);

            IExecutable exe = (Command)Activator.CreateInstance(typeOfCommand, parametersForConstructor);

            FieldInfo[] fieldsOfCommand = typeOfCommand.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            FieldInfo[] fieldsOfInterpreter = typeOfInterpreter.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo fieldOfCommand in fieldsOfCommand)
            {
                Attribute attr = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));

                if(attr != null)
                {
                    if(fieldsOfInterpreter.Any(f => f.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(exe, fieldsOfInterpreter
                            .First(f => f.FieldType == fieldOfCommand.FieldType).GetValue(this));
                    }
                }
            }

            return exe;
        }
    }
}
