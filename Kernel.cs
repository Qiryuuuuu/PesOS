using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Sys = Cosmos.System;
using System.Buffers;
using System.Diagnostics;
using static BasicCalculator;

namespace PesOS
{
    public class Kernel : Sys.Kernel
    {
        private List<User> users;
        private User currentUser;
        private FileSystem fileSystem;
        private MemoryManager memoryManager;


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
        private void Reboot()
        {
            Console.WriteLine("Rebooting PesOS...");
            Sys.Power.Reboot();
            // Restart the initial part of your operating system
            BeforeRun();
            // Indicate that the reboot is complete
            Console.WriteLine("PesOS rebooted successfully");
        }

        // This method is called before the main execution starts
        protected override void BeforeRun()
        {
            users = new List<User>();
            currentUser = null;
            fileSystem = new FileSystem();
            // Initialize Memory Manager with a pool size of 1024 bytes
            memoryManager = new MemoryManager(1024);
            // Create a sample user
            users.Add(new User("admin", "admin123"));

            ShowCenteredTitle("Innovation is the new Currency", 150);
            logoLoadingScreen();

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
                        Console.WriteLine("reboot - automatically reboot the operating system");
                        Console.WriteLine("shutdown - automatically shutdowns the operating system");
                        Console.WriteLine("file - displays the available command for file system");
                        Console.WriteLine("allocatememory [size] - allocates a block of memory of the specified size");
                        Console.WriteLine("freememory [address] - frees the memory block at the specified address");
                        Console.WriteLine("listmemory - lists all allocated memory blocks with their addresses and sizes.");
                        Console.WriteLine("settings - list of available modifications");
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

                    case "calculators":
                        Console.WriteLine("These are the available calculators");
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Basic Calculator");
                        Console.WriteLine("2 - Tax Calculator");

                        Console.Write("input: ");
                        var calInput = Console.ReadLine();

                        if (calInput == "1")
                        {
                            Console.Write("\nPesOS Calculator" + "\nInput your operation \n" +
                                "Type '+' for Addition\n" +
                                "Type '-' for Subtraction\n" +
                                "Type '*' for Multiplication\n" +
                                "Type '/' for Division\n" +
                                "\nInput: ");
                            var inputOperation = Console.ReadLine();

                            BasicCalculator basicCalcu = new BasicCalculator();
                            basicCalcu.calculator(inputOperation);
                        }

                        else if (calInput == "2")
                        {
                            Console.WriteLine("Type 'taxtable' to display the 2023 taxtable and 'taxtermino' for terminologies");
                            do
                            {
                                Console.Write("Enter Annual Income: ");
                                var incomeStr = Console.ReadLine();

                                TaxCalculator taxCalculator = new TaxCalculator();
                                if (incomeStr == "taxtable")
                                {
                                    taxCalculator.taxTable();
                                }else if (incomeStr == "taxtermino")
                                {
                                    taxCalculator.taxTerminologies();
                                }
                                taxCalculator.CalculateTax(incomeStr);

                                Console.WriteLine("Do you want to calculate tax again? (Type 'Yes' or 'No')");
                                var repeat = Console.ReadLine().Trim().ToLower();

                                if (repeat != "yes" && repeat != "y")
                                {
                                    break;
                                }

                            } while (true);
                        }
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "restart":
                        Sys.Power.Reboot();
                        break;

                    case "shutdown":
                        Console.Write("Are you sure you want to shut down? Type 'Yes' or 'No': ");
                        var shutdownConfirmation = Console.ReadLine().Trim().ToLower();
                        if (shutdownConfirmation == "yes" || shutdownConfirmation == "y")
                        {
                            Sys.Power.Shutdown();
                        }
                        else
                        {
                            Console.WriteLine("Shutdown canceled.");
                        }
                        break;

                    case "file":
                        Console.WriteLine("These are the available file system commands");
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Create File");
                        Console.WriteLine("2 - Read File");
                        Console.WriteLine("3 - Delete File");
                        Console.WriteLine("4 - Create Directory");
                        Console.WriteLine("5 - Change Directory");
                        Console.WriteLine("6 - List Files");

                        Console.Write("input: ");
                        var userInput = Console.ReadLine();
                        if (userInput == "0")
                        {
                            break;
                        }

                        else if (userInput == "1")
                        {
                            Console.Write("Enter File Name: ");
                            string filename = Console.ReadLine();
                            Console.Write("Enter File Content: ");
                            string fileContent = Console.ReadLine();
                            fileSystem.CreateFile(filename, fileContent);
                        }

                        else if (userInput == "2")
                        {
                            Console.Write("Enter file name to read: ");
                            string readFileName = Console.ReadLine();
                            fileSystem.ReadFile(readFileName);
                        }

                        else if (userInput == "3")
                        {
                            Console.Write("Enter file name to delete:");
                            string deleteFileName = Console.ReadLine();
                            fileSystem.DeleteFile(deleteFileName);
                        }

                        else if (userInput == "4")
                        {
                            Console.Write("Enter directory name: ");
                            string directoryName = Console.ReadLine();
                            fileSystem.CreateDirectory(directoryName);
                        }

                        else if (userInput == "5")
                        {
                            Console.Write("Enter directory name to change to: ");
                            string changeToDirectoryName = Console.ReadLine();
                            fileSystem.ChangeDirectory(changeToDirectoryName);
                        }

                        else if (userInput == "6"){
                            fileSystem.ListFilesAndDirectories();
                        }

                        else
                        {
                            Console.WriteLine("Choose valid option");
                        }

                        break;

                    // Add these cases to properly integrate the file system command
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
                    case "allocatememory":
                        if (splitted.Length > 1)
                        {
                            int size;
                            if (int.TryParse(splitted[1], out size))
                            {
                                int address = memoryManager.Allocate(size);
                                Console.WriteLine($"Allocated {size} bytes at address {address}.");
                            }
                            else
                            {
                                Console.WriteLine("Usage: allocatememory [size]");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usage: allocatememory [size]");
                        }
                        break;

                    case "freememory":
                        if (splitted.Length > 1)
                        {
                            int address;
                            if (int.TryParse(splitted[1], out address))
                            {
                                memoryManager.Free(address);
                                Console.WriteLine($"Freed memory at address {address}.");
                            }
                            else
                            {
                                Console.WriteLine("Usage: freememory [address]");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Usage: freememory [address]");
                        }
                        break;


                    case "settings":
                        string[] availableColors = { "white", "black", "gray", "yellow", "red", "blue", "green", "magenta", "cyan",
                         "darkred", "darkblue", "darkgreen", "darkmagenta", "darkcyan" };

                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Change color\n");
                        Console.Write("input: ");
                        var setInput = Console.ReadLine();

                        if (setInput == "1")
                        {
                            Console.WriteLine("0 - Back");
                            Console.WriteLine("1 - Change Font Color");
                            Console.WriteLine("2 - Change Background Color");

                            Console.Write("input: ");
                            var color = Console.ReadLine();

                            if (color == "0")
                            {
                                Console.Clear();
                                Console.WriteLine("Welcome to PesOS");
                                break;
                            }
                            else if (color == "1")
                            {
                                settings mySettings = new settings();

                                Console.WriteLine("Available Colors:");
                                settings.PrintAvailableColors(availableColors);

                                Console.Write("Enter the desired font color: ");
                                var fontColor = Console.ReadLine();

                                // Call the FontColor method to change the font color
                                mySettings.FontColor(new string[] { fontColor });
                                
                            }
                            else if (color == "2")
                            {
                                // Code for changing background color goes here
                                settings mySettings = new settings();

                                Console.WriteLine("Available Colors:");
                                settings.PrintAvailableColors(availableColors);

                                Console.Write("Enter the desired font color: ");
                                var backgroundColor = Console.ReadLine();

                                mySettings.BackgroundColor(new string[] { backgroundColor });
                                Console.Clear();

                            }
                            else
                            {
                                Console.WriteLine("Please choose a valid option");
                            }
                        }
                        else if (setInput == "0")
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome to PesOS");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option");
                        }
                        break;


                    case "reboot":
                        Console.Write("Are you sure you want to reboot? Type 'Yes' or 'No': ");
                        var rebootConfirmation = Console.ReadLine().Trim().ToLower();
                        if (rebootConfirmation == "yes" || rebootConfirmation == "y")
                        {
                            Reboot();
                        }
                        else
                        {
                            Console.WriteLine("Reboot canceled.");
                        }
                        break;
                    default:
                        Console.WriteLine("Command not found");
                        break;

                }

            }
        }



        private void logoLoadingScreen()
        {
            string text = @"                                                                    
             ++++                                +++-                      
            ++-------                        ++-+++--- ++    +++++----     
           ++-----------                    ++------------  ++----------   
           +++---   --++                    ++------+----- ++----- +-----  
           ++----------- +++++++ +++++++  ++-----    ++---+ +------++      
            ++--------   ++++    ++   +++ ++-----    ++-----++----------   
            ++----++     ++++++  ++++++++   ++----++++----  +++    +-----  
            ++---        +++     +++   ++  ++------------- ++-----++-----  
            ++---        +++++++ ++++++++    +- +-----+--   ++----------   
                                                 ++--           -----      
                                                                





 


  Created by:                                                     Version 1.0.0
  Alambra, Aragon, Banal, Beron, Bolocon, and De Guzman          S.Y. 2023-2024
    ";

            // Split the text into lines
            string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // Determine the maximum length of a line
            int maxLength = lines.Max(line => line.Length);

            // Calculate the starting position for centering horizontally and vertically
            int x = Console.WindowWidth / 2 - maxLength / 2;
            int y = Console.WindowHeight / 2 - lines.Length / 2;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Loop through each line and animate
            foreach (var line in lines)
            {
                // Check if the current position is within the console buffer
                if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(line, Console.ForegroundColor);
                    Console.ResetColor();
                }

                Thread.Sleep(300); // Adjust the sleep duration for the desired speed
                y++;
            }

            // Sleep for a moment before clearing the screen
            Thread.Sleep(2800);

            // Clear the screen
            Console.Clear();
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

public class MemoryManager
{
    private byte[] memoryPool;
    private List<MemoryBlock> allocatedBlocks;

    public MemoryManager(int poolSize)
    {
        memoryPool = new byte[poolSize];
        allocatedBlocks = new List<MemoryBlock>();
    }

    public int Allocate(int size)
    {
        // Find a free block in the memory pool
        int index = FindFreeBlock(size);

        // Allocate memory if a suitable block is found
        if (index != -1)
        {
            MemoryBlock block = new MemoryBlock(index, size);
            allocatedBlocks.Add(block);
            Console.WriteLine($"Allocated {size} bytes at address {index}.");
            return index;
        }
        else
        {
            Console.WriteLine("Memory allocation failed. Not enough free space.");
            return -1;
        }
    }

    public void Free(int address)
    {
        // Find and remove the block associated with the given address
        MemoryBlock block = allocatedBlocks.Find(b => b.Address == address);
        if (block != null)
        {
            allocatedBlocks.Remove(block);
            Console.WriteLine($"Freed memory at address {address}.");
        }
        else
        {
            Console.WriteLine($"Memory at address {address} not found.");
        }
    }

    private int FindFreeBlock(int size)
    {
        // Simple first-fit allocation strategy
        for (int i = 0; i < memoryPool.Length - size; i++)
        {
            if (!allocatedBlocks.Any(b => b.Contains(i, size)))
            {
                return i;
            }
        }
        return -1;
    }
}

public class MemoryBlock
{
    public int Address { get; private set; }
    public int Size { get; private set; }

    public MemoryBlock(int address, int size)
    {
        Address = address;
        Size = size;
    }

    public bool Contains(int address, int size)
    {
        return address >= Address && address + size <= Address + Size;
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
                decimal result = income - (.15m * (income - 250000));
                Console.WriteLine($"Your Annual Net Income is: {result}");
            }
            else if (income > 400000 && income <= 800000)
            {
                decimal result = income - (.20m * (income - 400000) + 22500);
                Console.WriteLine($"Your Annual Net Income is: {result}");
            }
            else if (income > 800000 && income <= 2000000)
            {
                decimal result = income - (.25m * (income - 800000) + 102500);
                Console.WriteLine($"Your Annual Net Income is: {result}");
            }
            else if (income > 2000000 && income <= 8000000)
            {
                decimal result = income - (.30m * (income - 2000000) + 402500);
                Console.WriteLine($"Your Annual Net Income is: {result}");
            }
            else if (income > 8000000)
            {
                decimal result = income - (.35m * (income - 8000000) + 2202500);
                Console.WriteLine($"Your Annual Net Income is: {result}");
            }
        } 


        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number for income.\n");
        }

    }

    public void taxTable()
    {
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
    }

    public void taxTerminologies()
    {
        Console.WriteLine("These are the terminologiesa");
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

    public class settings
    {
        public void FontColor(string[] args)
        {
            string[] availableColors = { "white", "black", "gray", "yellow", "red", "blue", "green", "magenta", "cyan",
                    "darkred", "darkblue", "darkgreen", "darkmagenta", "darkcyan" };
            string response = "";

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "white":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("The console's color has now changed to White");
                        break;

                    case "black":
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    case "gray":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("The console's color has now changed to Gray");
                        break;

                    case "yellow":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("The console's color has now changed to Yellow");
                        break;

                    case "red":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The console's color has now changed to Red");
                        break;

                    case "darkred":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The console's color has now changed to Cyan");
                        break;

                    case "blue":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("The console's color has now changed to Blue");
                        break;

                    case "darkblue":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("The console's color has now changed to Dark Blue");
                        break;

                    case "green":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The console's color has now changed to Green");
                        break;

                    case "darkgreen":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("The console's color has now changed to Dark Green");
                        break;

                    case "magenta":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    case "darkmagenta":
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    case "cyan":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("The console's color has now changed to Cyan");
                        break;

                    case "darkcyan":
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    default:
                        response = "The color \"" + args[0] + "\" is not available";
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please provide a color as an argument.");
                PrintAvailableColors(availableColors);
            }


        }
   
        public void BackgroundColor(string[] args)
        {
            string[] availableColors = {"white", "black", "gray", "yellow", "red", "blue", "green", "magenta", "cyan",
                    "darkred", "darkblue", "darkgreen", "darkmagenta", "darkcyan" };
            string response = "";

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "white":
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("The console's color has now changed to White");
                        break;

                    case "black":
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    case "gray":
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine("The console's color has now changed to Gray");
                        break;

                    case "yellow":
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("The console's color has now changed to Yellow");
                        break;

                    case "red":
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("The console's color has now changed to Red");
                        break;

                    case "darkred":
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("The console's color has now changed to Cyan");
                        break;

                    case "blue":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine("The console's color has now changed to Blue");
                        break;

                    case "darkblue":
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("The console's color has now changed to Dark Blue");
                        break;

                    case "green":
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.WriteLine("The console's color has now changed to Green");
                        break;

                    case "darkgreen":
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("The console's color has now changed to Dark Green");
                        break;

                    case "magenta":
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    case "darkmagenta":
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    case "cyan":
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("The console's color has now changed to Cyan");
                        break;

                    case "darkcyan":
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("The console's color has now changed to Magenta");
                        break;

                    default:
                        response = "The color \"" + args[0] + "\" is not available";
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please provide a color as an argument.");
                PrintAvailableColors(availableColors);
            }


        }
        public static void PrintAvailableColors(string[] colors)
        {
            Console.WriteLine("You can choose between these colors:");
            foreach (var color in colors)
            {
                Console.WriteLine($"\t{color}");
            }
        }
    }



}
