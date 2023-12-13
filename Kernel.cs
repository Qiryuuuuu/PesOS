using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;


namespace PesOS
{
    public class Kernel : Sys.Kernel
    {
        private List<User> users;
        private User currentUser;


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
                        Console.Clear();
                        Console.WriteLine("These are the available commands of PesOS:");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("help - displays a list of available commands");
                        Console.WriteLine("clock - displays the current date and time");
                        Console.WriteLine("tax - displays the available tax features");
                        Console.WriteLine("clear - clears the command line");
                        Console.WriteLine("restart - automatically restarts the operating system");
                        Console.WriteLine("shutdown - automatically shutdowns the operating system");
                        Console.WriteLine("sysinfo - displays the system information of PesOS");
                        Console.WriteLine("settings - displays the available system modifications");
                        Console.WriteLine("--------------------------------------------------------");
                        break;


                    case "clock":
                        DateTime clock = DateTime.Now;
                        Console.WriteLine("\nThe current date and time is: " + clock);
                        break;

                    case "tax":
                        Console.Clear();
                        Console.WriteLine("These are the available tax features");
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - PesOS Calculator");
                        Console.WriteLine("2 - Tax Calculator");

                        Console.Write("input: ");
                        var calInput = Console.ReadLine();

                        if (calInput == "1")
                        {
                            Console.Clear();
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
                            Console.WriteLine("\nType 'taxtable' to display the Income Tax Rates and 'taxterms' for terminologies");

                            do
                            {
                                Console.Write("Enter Annual Gross Income: ");
                                var incomeStr = Console.ReadLine();

                                TaxCalculator taxCalculator = new TaxCalculator();
                                taxCalculator.CalculateTax(incomeStr);

                                Console.WriteLine("Do you want to calculate your tax? (Type 'Yes' or 'No')");
                                Console.Write("input: ");
                                var repeat = Console.ReadLine().Trim().ToLower();

                                if (repeat == "no" || repeat == "n")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Tax Calculator closed");
                                    Console.WriteLine("--------------------------------------------------------\n");
                                    Console.WriteLine("Welcome to PesOS");
                                    Console.WriteLine("Type 'help' to view available commands");
                                    break;
                                }
                                else if (repeat == "yes" || repeat == "y")
                                {
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please choose between 'Yes' or 'No' only.");
                                }

                            } while (true);
                        }

                        else if (calInput == "0") 
                        { 
                            Console.Clear();
                            Console.WriteLine("Welcome to PesOS");
                            Console.WriteLine("Type 'help' to view available commands");
                            break;
                        }

                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please input a valid option");
                            Console.WriteLine("--------------------------------------------------------\n");
                            Console.WriteLine("Welcome to PesOS");
                            Console.WriteLine("Type 'help' to view available commands");
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

                    case "settings":
                        string[] availableColors = { "white", "black", "gray", "red", "blue", "green", "magenta", "cyan",
                         "darkred", "darkblue", "darkgreen", "darkmagenta", "darkcyan" };

                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Change Color\n");
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
           ++----++     ++++++ ++++++++   ++---++++-----   +++    +-----  
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
            Console.WriteLine("PesOS requires authentication");

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
                        Console.WriteLine("Max attempts have been reached. The system will restart.");
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

    }
}


