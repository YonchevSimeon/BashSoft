namespace BashSoft
{
    using BashSoft.IO;
    using BashSoft.Judge;
    using BashSoft.Contracts;
    using BashSoft.Repository;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            #region AdvancedTests
            ///
            ///Testing Traversing A Folder
            ///Works
            ///
            //IOManager.TraverseDirectory();

            ///
            ///Testing Get All Students 
            ///Works kinda
            ///
            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentsFromCourse("Unity");
            //StudentsRepository.GetStudentScoresFromCourse("Unity", "Ivan");

            ///
            ///Testing Comparing Files
            ///Works
            ///
            //Tester.CompareContent(@"E:\Projects\BashSoft\BashSoft\BashSoft\resources\test2.txt"
            //                    , @"E:\Projects\BashSoft\BashSoft\BashSoft\resources\test3.txt");

            // cmp E:\Projects\BashSoft\BashSoft\BashSoft\resources\test1.txt E:\Projects\BashSoft\BashSoft\BashSoft\resources\test2.txt

            ///
            ///Testing Creating Folders And Traversing Folders
            ///Works
            ///
            //IOManager.CreateDirectoryInCurrentFolder("*2");
            //IOManager.ChangeCurrentDirectoryAbsolute(@"C:\Windows");
            //IOManager.TraverseDirectory(20);

            ///
            ///Testing Going one folder up the hierarchy
            ///Works
            ///
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.ChangeCurrentDirectoryRelative("..");

            ///
            ///Testing InputReader and CommandInterpreter
            ///Works
            ///
            #endregion

            IContentComparer tester = new Tester();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentsRepository(new RepositoryFilter(), new RepositorySorter());

            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();

            ///
            ///Tips
            ///use cdrel resources
            ///then read files from there with readdb 
            ///or help to see other commands
            ///
        }
    }
}
