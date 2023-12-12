using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesOS
{
    public class FileSystem
    {
        private Directory currentDirectory;

        public FileSystem()
        {
            currentDirectory = new Directory("root");
        }

        public void CreateFile(string fileName, string content)
        {
            File newFile = new File(fileName, content);
            currentDirectory.AddFile(newFile);
            Console.WriteLine($"File '{fileName}' created successfully.");
        }

        public void ReadFile(string fileName)
        {
            File file = currentDirectory.GetFile(fileName);
            if (file != null)
            {
                Console.WriteLine($"Content of '{fileName}':");
                Console.WriteLine(file.Content);
            }
            else
            {
                Console.WriteLine($"File '{fileName}' not found.");
            }
        }

        public void DeleteFile(string fileName)
        {
            if (currentDirectory.RemoveFile(fileName))
            {
                Console.WriteLine($"File '{fileName}' deleted successfully.");
            }
            else
            {
                Console.WriteLine($"File '{fileName}' not found.");
            }
        }

        public void CreateDirectory(string directoryName)
        {
            Directory newDirectory = new Directory(directoryName);
            currentDirectory.AddDirectory(newDirectory);
            Console.WriteLine($"Directory '{directoryName}' created successfully.");
        }

        public void ChangeDirectory(string directoryName)
        {
            Directory newDirectory = currentDirectory.GetDirectory(directoryName);
            if (newDirectory != null)
            {
                currentDirectory = newDirectory;
                Console.WriteLine($"Changed to directory '{directoryName}'.");
            }
            else
            {
                Console.WriteLine($"Directory '{directoryName}' not found.");
            }
        }

        public void ListFilesAndDirectories()
        {
            Console.WriteLine($"Contents of '{currentDirectory.Name}':");
            foreach (File file in currentDirectory.Files)
            {
                Console.WriteLine($"File: {file.Name}");
            }
            foreach (Directory directory in currentDirectory.Subdirectories)
            {
                Console.WriteLine($"Directory: {directory.Name}");
            }
        }
    }

    public class Directory
    {
        public string Name { get; private set; }
        public List<File> Files { get; private set; }
        public List<Directory> Subdirectories { get; private set; }

        public Directory(string name)
        {
            Name = name;
            Files = new List<File>();
            Subdirectories = new List<Directory>();
        }

        public void AddFile(File file)
        {
            Files.Add(file);
        }

        public File GetFile(string fileName)
        {
            return Files.Find(f => f.Name == fileName);
        }

        public bool RemoveFile(string fileName)
        {
            File fileToRemove = GetFile(fileName);
            if (fileToRemove != null)
            {
                Files.Remove(fileToRemove);
                return true;
            }
            return false;
        }

        public void AddDirectory(Directory directory)
        {
            Subdirectories.Add(directory);
        }

        public Directory GetDirectory(string directoryName)
        {
            return Subdirectories.Find(d => d.Name == directoryName);
        }
    }

    public class File
    {
        public string Name { get; private set; }
        public string Content { get; set; }

        public File(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
