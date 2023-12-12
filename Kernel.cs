using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Sys = Cosmos.System;


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
            Console.Beep(800, 300);
            Console.Beep(600, 400);
            Console.Beep(400, 500);
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
            Console.Beep(400, 300);
            Console.Beep(800, 400);
            Console.Beep(600, 500);
            logoLoadingScreen();

            // Authenticate the user
            while (currentUser == null)
            {
                AuthenticateUser();
            }
            Console.WriteLine($"Welcome, {currentUser.Username}!");
            Console.WriteLine("You have successfully logged-in to PesOS.");
            Console.WriteLine("Type 'help' to view available commands");
        }
        private bool LogoutAndAuthenticate()
        {
            currentUser.Logout();
            currentUser = null;

            // Authenticate the user again
            AuthenticateUser();

            return currentUser != null;
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
                        Console.WriteLine("These are the available commands of PesOS:");
                        Console.WriteLine("help - displays a list of available commands");
                        Console.WriteLine("sysinfo - displays the system information of PesOS");
                        Console.WriteLine("clock - displays the current date and time");
                        Console.WriteLine("tax - displays the available tax features");
                        Console.WriteLine("file - displays the available command for file system");
                        Console.WriteLine("clear - clears the command line");
                        Console.WriteLine("restart - automatically restarts the operating system");
                        Console.WriteLine("shutdown - automatically shutdowns the operating system");
                        Console.WriteLine("user - displays the available command for user profile");
                        Console.WriteLine("settings - displays the available system modifications");
                        //add more for added features
                        break;


                    case "clock":
                        DateTime clock = DateTime.Now;
                        Console.WriteLine("\nThe current date and time is: " + clock);
                        break;

                    case "tax":
                        Console.WriteLine("These are the available tax features");
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - PesOS Calculator");
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
                            Console.WriteLine("\nType 'taxtable' to display the 2023 taxtable and 'taxterms' for terminologies");
                            do
                            {
                                Console.Write("Enter Annual Income: ");
                                var incomeStr = Console.ReadLine();

                                TaxCalculator taxCalculator = new TaxCalculator();

                                taxCalculator.CalculateTax(incomeStr);

                                Console.WriteLine("Do you want to calculate your tax? (Type 'Yes' or 'No')");
                                Console.Write("input: ");
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
                        Console.WriteLine("Welcome to PesOS");
                        Console.WriteLine("Type 'help' to view available commands");
                        break;

                    case "shutdown":
                        Console.Write("Are you sure you want to shut down? Type 'Yes' or 'No': ");
                        var shutdownConfirmation = Console.ReadLine().Trim().ToLower();
                        if (shutdownConfirmation == "yes" || shutdownConfirmation == "y")
                        {
                            Console.Beep(800, 300);
                            Console.Beep(600, 400);
                            Console.Beep(400, 500);
                            Sys.Power.Shutdown();
                        }
                        else
                        {
                            Console.WriteLine("Shutdown canceled.");
                        }
                        break;

                    case "file":
                        Console.WriteLine("These are the available file system commands:");
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Create File");
                        Console.WriteLine("2 - Read File");
                        Console.WriteLine("3 - Delete File");
                        Console.WriteLine("4 - Create Directory");
                        Console.WriteLine("5 - Change Directory");
                        Console.WriteLine("6 - List Files");

                        Console.Write("input: ");
                        var fileInput = Console.ReadLine();
                        if (fileInput == "0")
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome to PesOS");
                            Console.WriteLine("Type 'help' to view available commands");
                            break;
                        }

                        else if (fileInput == "1")
                        {
                            Console.Write("Enter File Name: ");
                            string filename = Console.ReadLine();
                            Console.Write("Enter File Content: ");
                            string fileContent = Console.ReadLine();
                            fileSystem.CreateFile(filename, fileContent);
                        }

                        else if (fileInput == "2")
                        {
                            Console.Write("Enter file name to read: ");
                            string readFileName = Console.ReadLine();
                            fileSystem.ReadFile(readFileName);
                        }

                        else if (fileInput == "3")
                        {
                            Console.Write("Enter file name to delete:");
                            string deleteFileName = Console.ReadLine();
                            fileSystem.DeleteFile(deleteFileName);
                        }

                        else if (fileInput == "4")
                        {
                            Console.Write("Enter directory name: ");
                            string directoryName = Console.ReadLine();
                            fileSystem.CreateDirectory(directoryName);
                        }

                        else if (fileInput == "5")
                        {
                            Console.Write("Enter directory name to change to: ");
                            string changeToDirectoryName = Console.ReadLine();
                            fileSystem.ChangeDirectory(changeToDirectoryName);
                        }

                        else if (fileInput == "6")
                        {
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

                    case "user":
                        Console.WriteLine("These are the available user commands:");
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Change Username");
                        Console.WriteLine("2 - Change Password");
                        Console.WriteLine("3 - Change Host Identity");
                        Console.WriteLine("4 - Display User Info");
                        Console.WriteLine("5 - Create New User");
                        Console.WriteLine("6 - Logout");

                        Console.Write("input: ");
                        var userInput = Console.ReadLine();
                        if (userInput == "0")
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome to PesOS");
                            Console.WriteLine("Type 'help' to view available commands");
                            break;
                        }

                        else if (userInput == "1")
                        {
                            Console.Write("Enter new username: ");
                            string newUsername = Console.ReadLine();
                            currentUser.ChangeUsername(newUsername);
                        }

                        else if (userInput == "2")
                        {
                            Console.Write("Enter new password: ");
                            string newPassword = Console.ReadLine();
                            currentUser.ChangePassword(newPassword);
                        }

                        else if (userInput == "3")
                        {
                            currentUser.ChangeHostIdentity();
                        }

                        else if (userInput == "4")
                        {
                            DisplayUserInfo();
                        }

                        else if (userInput == "5")
                        {
                            Console.Write("Enter new username: ");
                            string createUsername = Console.ReadLine();
                            Console.Write("Enter password: ");
                            string createPassword = Console.ReadLine();

                            // Create a new user and log them in
                            currentUser = User.CreateUser(createUsername, createPassword);
                            Console.WriteLine($"User {createUsername} created and logged in.");
                        }

                        else if (userInput == "6")
                        {
                            if (LogoutAndAuthenticate())
                            {
                                Console.WriteLine($"Welcome back, {currentUser.Username}!");
                            }
                        }

                        else
                        {
                            Console.WriteLine("Choose valid option");
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
                                Console.WriteLine("Type 'help' to view available commands");
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

                                Console.Write("Enter the desired background color: ");
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
                            Console.WriteLine("Type 'help' to view available commands");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please choose a valid option");
                        }
                        break;


                    case "restart":
                        Console.Write("Are you sure you want to restart? Type 'Yes' or 'No': ");
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

                    case "sysinfo":
                        SysInfo sysDes = new SysInfo();
                        sysDes.SystemInfo();
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
             ++++                               +++-                      
            ++-------                        ++-+++---++      +++++----     
           ++-----------                  ++------------    ++----------   
           +++--   ---++                  ++------+------  ++----- +-----  
           ++----------- ++++++ +++++++  ++-----    ++---+  +------++      
            ++--------   +++    +++   ++ ++-----    ++----- ++----------   
            ++----++     ++++++ ++++++++   ++----++++----   +++    +-----  
            ++---        +++    ++   +++  ++-------------  ++-----++-----  
            ++---        ++++++  +++++++    +- +-----+--    ++----------   
                                                ++--            -----      
                                                                





 


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

            int maxAttempts = 5;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                currentUser = users.Find(u => u.Username == username && u.ValidatePassword(password));

                if (currentUser == null)
                {
                    attempts++;
                    Console.Beep(800, 150);
                    Console.Beep(600, 200);

                    Console.Clear();
                    Console.WriteLine($"Authentication Failed. Attempts remaining: {maxAttempts - attempts}");

                    if (attempts == maxAttempts)
                    {
                        Console.WriteLine("Max attempts has been reached. System will restart");
                        Reboot();
                    }
                }
                else
                {
                    Console.Beep(600, 250);
                    Console.Beep(800, 300);
                    Console.Clear();
                    break;
                }
            }

        }
        private void DisplayUserInfo()
        {
            if (currentUser != null)
            {
                currentUser.DisplayUserInfo();
            }
            else
            {
                Console.WriteLine("No user is currently logged in.");
            }


        }


    }
}



