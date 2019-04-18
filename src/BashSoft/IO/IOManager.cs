namespace BashSoft.IO
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using BashSoft.StaticData;

    public class IOManager : IDirectoryManager
    {
        public void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.currentPath.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);
            while(subFolders.Count != 0)
            {
                string currentPath = subFolders.Dequeue();
                int identation = currentPath.Split('\\').Length - initialIdentation;

                if(depth - identation < 0)
                {
                    break;
                }
                //C# Advanced----->>>>
                //Gives error with the indexes if you use the commented way of printing!!1
                //OutputWriter.WriteMessageOnNewLine(string.Format($"{new string('-', identation)}{currentPath}"));
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));
                try
                {
                    foreach (string file in Directory.GetFiles(currentPath))
                    {
                        int indexOfLastSlash = file.LastIndexOf("\\");
                        string fileName = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + fileName);
                    }
                    string[] subDirectories = Directory.GetDirectories(currentPath);
                    foreach (string subDirectory in subDirectories)
                    {
                        subFolders.Enqueue(subDirectory);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            string path = SessionData.currentPath + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
                OutputWriter.WriteMessageOnNewLine($"Folder {name} created.");
            }
            catch(ArgumentException)
            {
                throw new InvalidFileNameException();
            }
        }
        
        public void ChangeCurrentDirectoryRelative (string relativePath)
        {
            if(relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;
                    int indexOfLastSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new InvalidPathException();
                }
            }
            else
            {
                string currentPath = SessionData.currentPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new InvalidPathException();
            }
            SessionData.currentPath = absolutePath;
        }
    }
}
