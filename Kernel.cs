using System;
using System.Collections.Generic;
using System.Threading;
using Sys = Cosmos.System;

namespace PesOS
{
    public class Kernel : Sys.Kernel
    {
        private List<User> users;
        private User currentUser;
        private FileSystem fileSystem;
        private void ShowCenteredTitle(string title, int speed)
        {
            Console.Clear();

            // Calculate the horizontal and vertical position for a centered display
            int leftMargin = (Console.WindowWidth - title.Length) / 2;
            Console.SetCursorPosition(leftMargin, Console.WindowHeight / 2);

            foreach (char letter in title)
            {
                Console.Write(letter);
                Thread.Sleep(speed);
            }

            Console.WriteLine();
            Thread.Sleep(2000);
            Console.Clear();
        }

        // This method is called before the main execution starts
        protected override void BeforeRun()
        {
            users = new List<User>();
            currentUser = null;
            fileSystem = new FileSystem();
            // Create a sample user
            users.Add(new User("admin", "admin123"));

            ShowCenteredTitle("Innovation is the new Currency", 150);
            ShowCenteredTitle("PesOS", 400);

            // Authenticate the user
            while (currentUser == null)
            {
                AuthenticateUser();
            }

            Console.WriteLine($"Welcome, {currentUser.Username}!");
            Console.WriteLine($"Host Identity Code: {currentUser.HostIdentityCode}");
            Console.WriteLine("");

            Console.WriteLine("PesOS booted successfully");
            Console.WriteLine("Type 'help' to view available commands");
        }

        // This method contains the main execution logic
        protected override void Run()
        {
            while (true)
            {
                Console.WriteLine("");
                Console.Write("Input: ");
                var input = Console.ReadLine();

                string[] splitted = input.Split(" ");
                switch (splitted[0])
                {
                    case "help":
                        Console.WriteLine("These are the available commands of PesOS");
                        Console.WriteLine("help - display the available commands");
                        Console.WriteLine("info - display information of the Operating system");
                        Console.WriteLine("clock - display the current date and time");
                        Console.WriteLine("calculator - enables the user to utilize a basic calculator");
                        Console.WriteLine("taxtable - display the individual tax description in table form");
                        Console.WriteLine("taxcalcu - calculate the user's income after tax");
                        Console.WriteLine("clear - clear the console");
                        Console.WriteLine("restart - automatically restarts the operating system");
                        Console.WriteLine("shutdown - automatically shutdowns the operating system");
                        Console.WriteLine("createfile - create a file");
                        Console.WriteLine("readfile - read a file");
                        Console.WriteLine("deletefile - delete a file");
                        Console.WriteLine("createdirectory - create a directory");
                        Console.WriteLine("changedirectory - change a directory");
                        Console.WriteLine("listfiles - list all files");
                        //add more for added features
                        break;

                    case "info":
                        Console.WriteLine("This is a Personalized Operating System");
                        Console.WriteLine("Created by - Alambra, Aragon, Banal, Beron, Bolocon, and De Guzman");
                        Console.WriteLine("S.Y. 2023-2024");
                        break;

                    case "clock":
                        DateTime clock = DateTime.Now;
                        Console.WriteLine(clock);
                        break;

                    case "calculator":
                        Console.Write("\nPesOS Calculator" + "\nInput your operation \n" +
                            "Type '+' for Addition\n" +
                            "Type '-' for Subtraction\n" +
                            "Type '*' for Multiplication\n" +
                            "Type '/' for Division\n" +
                            "\nInput: ");
                        var inputOperation = Console.ReadLine();

                        BasicCalculator basicCalcu = new BasicCalculator();
                        basicCalcu.calculator(inputOperation);
                        break;

                    case "taxtable":
                        Console.WriteLine("The Income Tax Table - Individual");
                        Console.WriteLine("-------------------------------------------------------------------------");
                        Console.WriteLine("|   Over    | But not Over |                 Tax Rate                   |");
                        Console.WriteLine("-------------------------------------------------------------------------");
                        Console.WriteLine("|    ---    |    250,000   |                    0%                      |");
                        Console.WriteLine("|  250,000  |    400,000   |         15% of excess over 250,000         |");
                        Console.WriteLine("|  400,000  |    800,000   |     22,500 + 20% of excess over 400,000    |");
                        Console.WriteLine("|  800,000  |   2,000,000  |     102,500 + 25% of excess over 800,000   |");
                        Console.WriteLine("| 2,000,000 |   8,000,000  |    402,500 + 30% of excess over 2,000,000  |");
                        Console.WriteLine("| 8,000,000 |      ---     |  2,202,500 + 35% of excess over 8,000,000  |");
                        Console.WriteLine("-------------------------------------------------------------------------");
                        Console.WriteLine("Department of Finance - Tax Schedule Effective January 1, 2023 and onwards");
                        break;

                    case "taxcalcu":
                        do
                        {
                            Console.Write("Enter Annual Income: ");
                            var incomeStr = Console.ReadLine();

                            TaxCalculator taxCalculator = new TaxCalculator();
                            taxCalculator.CalculateTax(incomeStr);

                            Console.WriteLine("Do you want to calculate tax again? (Type 'Yes' or 'No')");
                            var repeat = Console.ReadLine().Trim().ToLower();

                            if (repeat != "yes" && repeat != "y")
                            {
                                break;
                            }

                        } while (true);
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "restart":
                        Sys.Power.Reboot();
                        break;

                    case "shutdown":
                        Sys.Power.Shutdown();
                        break;
                    case "createfile":
                        Console.Write("Enter file name: ");
                        string fileName = Console.ReadLine();
                        Console.Write("Enter file content: ");
                        string fileContent = Console.ReadLine();
                        fileSystem.CreateFile(fileName, fileContent);
                        break;

                    case "readfile":
                        Console.Write("Enter file name to read: ");
                        string readFileName = Console.ReadLine();
                        fileSystem.ReadFile(readFileName);
                        break;

                    case "deletefile":
                        Console.Write("Enter file name to delete: ");
                        string deleteFileName = Console.ReadLine();
                        fileSystem.DeleteFile(deleteFileName);
                        break;

                    case "createdirectory":
                        Console.Write("Enter directory name: ");
                        string directoryName = Console.ReadLine();
                        fileSystem.CreateDirectory(directoryName);
                        break;

                    case "changedirectory":
                        Console.Write("Enter directory name to change to: ");
                        string changeToDirectoryName = Console.ReadLine();
                        fileSystem.ChangeDirectory(changeToDirectoryName);
                        break;

                    case "listfiles":
                        fileSystem.ListFilesAndDirectories();
                        break;

                    // Add these cases to properly integrate the file system commands
                    case "mkdir":
                        if (splitted.Length > 1)
                        {
                            fileSystem.CreateDirectory(splitted[1]);
                        }
                        else
                        {
                            Console.WriteLine("Usage: mkdir [directoryName]");
                        }
                        break;

                    case "cd":
                        if (splitted.Length > 1)
                        {
                            fileSystem.ChangeDirectory(splitted[1]);
                        }
                        else
                        {
                            Console.WriteLine("Usage: cd [directoryName]");
                        }
                        break;

                    case "ls":
                        fileSystem.ListFilesAndDirectories();
                        break;
                    default:
                        Console.WriteLine("Command not found");
                        break;

                }

            }
        }

        private void AuthenticateUser()
        {
            Console.WriteLine("PesOS requires an authentication");
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            currentUser = users.Find(u => u.Username == username && u.ValidatePassword(password));


            if (currentUser == null)
            {
                Console.Clear();
                Console.WriteLine("Authentication failed. User not found or invalid credentials. Try again.");
            }
            else
            {
                Console.Clear();
            }
        }
    }
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



public class User
    {
        public string Username { get; private set; }
        private string PasswordHash { get; set; }
        public string HostIdentityCode { get; private set; }

        public User(string username, string password)
        {
            Username = username;
            PasswordHash = HashPassword(password);
            HostIdentityCode = Guid.NewGuid().ToString();
        }

        private string HashPassword(string password)
        {
            // Use a secure hashing algorithm (e.g., bcrypt) in a real-world scenario
            return password.GetHashCode().ToString();
        }

        public bool ValidatePassword(string inputPassword)
        {
            // Use a secure password verification mechanism in a real-world scenario
            return PasswordHash == HashPassword(inputPassword);
        }
    }

    //This is method contains the calculator for income tax 
    public class TaxCalculator
    {
        public void CalculateTax(string incomeStr)
        {
            if (int.TryParse(incomeStr, out int income))
            {
                if (income >= 0 && income <= 250000)
                {
                    Console.WriteLine($"There is no tax for income that is not over 250000");
                    Console.WriteLine($"Your Annual Income: {incomeStr}");
                }
                else if (income > 250000 && income <= 400000)
                {
                    decimal fifteenPercent = .15m * income;
                    decimal result = income - fifteenPercent;
                    Console.WriteLine($"15% of {incomeStr} is: {result}");
                }
                else if (income > 400000 && income <= 800000)
                {
                    decimal twentyPercent = 22500 + .20m * income;
                    decimal result = income - twentyPercent;
                    Console.WriteLine($"22,500 + 20% of {incomeStr} is: {result}");
                }
                else if (income > 800000 && income <= 2000000)
                {
                    decimal twentyFivePercent = 102500 + .25m * income;
                    decimal result = income - twentyFivePercent;
                    Console.WriteLine($"22,500 + 25% of {incomeStr} is: {result}");
                }
                else if (income > 2000000 && income <= 8000000)
                {
                    decimal thirtyPercent = 402500 + .30m * income;
                    decimal result = income - thirtyPercent;
                    Console.WriteLine($"402,500 + 30% of {incomeStr} is: {result}");
                }
                else if (income > 8000000)
                {
                    decimal thirtyFivePercent = 2202500 + .35m * income;
                    decimal result = income - thirtyFivePercent;
                    Console.WriteLine($"2,202,500 + 35% of {incomeStr} is: {result}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number for income.");
            }

        }
    }

    //This method contains the function of a basic calculator for two variables
    public class BasicCalculator
    {
        public void calculator(string inputOperation)
        {
            for (int i = 1; i == 1; i = i)
            {
                int firstValue, secondValue;

                if (inputOperation != "+" && inputOperation != "-" && inputOperation != "*" && inputOperation != "/")
                {
                    Console.WriteLine("Invalid operation, PesOS Calculator Closed.");
                    break;
                }

                Console.WriteLine($"Input two values to be ({inputOperation}).");

                Console.Write("First Value: ");
                if (!int.TryParse(Console.ReadLine(), out firstValue))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                    continue;
                }

                Console.Write("Second Value: ");
                if (!int.TryParse(Console.ReadLine(), out secondValue))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                    continue;
                }

                switch (inputOperation)
                {
                    case "+":
                        Console.WriteLine($"The Sum of {firstValue} and {secondValue} is: {firstValue + secondValue}");
                        break;
                    case "-":
                        Console.WriteLine($"The Difference of {firstValue} and {secondValue} is: {firstValue - secondValue}");
                        break;
                    case "*":
                        Console.WriteLine($"The Product of {firstValue} and {secondValue} is: {firstValue * secondValue}");
                        break;
                    case "/":
                        if (secondValue != 0)
                        {
                            Console.WriteLine($"The Quotient of {firstValue} and {secondValue} is: {firstValue / secondValue}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot divide by zero.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid operation, PesOS Calculator Closed.");
                        break;
                }

                Console.WriteLine("\nWould you like to use the calculator again? Type 'Yes' or 'No'.\n");

                for (int j = 1; j == 1; j = j)
                {
                    Console.Write("Input: ");
                    var reCalculate = Console.ReadLine().Trim();

                    if (reCalculate == "No")
                    {
                        Console.WriteLine("\nPesOS Calculator Closed");
                        i = 0; j = 0;

                    }
                    else if (reCalculate == "Yes")
                    {
                        i = 1; j = 0;
                    }
                    else
                    {
                        Console.WriteLine("Please input either 'Yes' or 'No' only.\n");
                        j = 1;
                    }
                }
            }
        }
    }
