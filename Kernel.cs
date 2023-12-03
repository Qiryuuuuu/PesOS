﻿using System;
using System.Collections.Generic;
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

        // This method is called before the main execution starts
        protected override void BeforeRun()
        {
            users = new List<User>();
            currentUser = null;

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
                        Console.WriteLine("These are the available commands in PesOS");
                        Console.WriteLine("The 'help' command displays a list of available commands");
                        Console.WriteLine("The 'info' command shows information about the Operating system");
                        Console.WriteLine("The 'clock' command displays the current date and time");
                        Console.WriteLine("The 'calculator' command enables the user to utilize a basic calculator");
                        Console.WriteLine("The 'clear' command clears the console by removing the previous texts");
                        Console.WriteLine("The 'restart' command automatically restarts the operating system");
                        Console.WriteLine("The 'shutdown' command automatically shuts down the operating system");
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
                        for (int i = 1; i == 1; i=i)
                        {
                            Console.Write("PesOS Calculator" + "\n\n" + "Input your operation" + "\n" +
                                "Type '+' for Addition\n" +
                                "Type '-' for Subtraction\n" +
                                "Type '*' for Multiplication\n" +
                                "Type '/' for Division\n" +
                                "\nInput: ");

                            var inputOperation = Console.ReadLine();

                            if (inputOperation == "+")
                            {
                                Console.Write("\nInput two values to be added.\n\n");
                                Console.Write("First Value: \n");
                                int firstAddend = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Second Value: \n");
                                int secondAddend = Convert.ToInt32(Console.ReadLine());
                                int sum = firstAddend + secondAddend;
                                Console.Write("\nThe Sum of First and Second Values is: " + sum + "\n");
                            }

                            else if (inputOperation == "-")
                            {
                                Console.Write("\nInput two values to be subtracted.\n\n");
                                Console.Write("Minuend Value: \n");
                                int minuend = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Subtrahend Value: \n");
                                int subtrahend = Convert.ToInt32(Console.ReadLine());
                                int difference = minuend - subtrahend;
                                Console.Write("\nThe Difference of First and Second Values is: " + difference + "\n");
                            }

                            else if (inputOperation == "*")
                            {
                                Console.Write("\nInput two values to be multiplied.\n\n");
                                Console.Write("Multiplicand Value: \n");
                                int multiplicand = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Multiplier Value: \n");
                                int multiplier = Convert.ToInt32(Console.ReadLine());
                                int product = multiplicand * multiplier;
                                Console.Write("\nThe Product of First and Second Values is: " + product + "\n");
                            }

                            else if (inputOperation == "/")
                            {
                                Console.Write("\nInput two values to be divided.\n\n");
                                Console.Write("Dividend Value: \n");
                                int dividend = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Divisor Value: \n");
                                int divisor = Convert.ToInt32(Console.ReadLine());
                                int quotient = dividend / divisor;
                                Console.Write("\nThe Quotient of First and Second Values is: " + quotient + "\n");
                            }

                            else
                            {
                                Console.Write("\nPlease input a valid calculator operation.\n");
                            }

                            Console.Write("\nWould you like to use the calculator again? \n" + "Type 'Yes' or 'No'.\n\n" + "Input: ");
                            var reCalculate = Console.ReadLine();
                            if (reCalculate == "Yes")
                            {
                                i = 1;
                            }

                            else if (reCalculate == "No")
                            {
                                i = 0;
                            }
                          
                            else
                            {

                                for (int j = 1; j == 1; j = j)

                                {
                                    Console.Write("\nPlease input either 'Yes' or 'No' only.\n" + "\nInput: ");
                                    var repeatInput = Console.ReadLine();
                                    if (repeatInput == "Yes")
                                    {
                                        i = 1; j = 0;
                                    }

                                    else if (repeatInput == "No")
                                    {
                                        i = 0; j = 0;
                                        break;
                                    }
                                    else
                                    {
                                        j = 1;
                                    }
                                }
                                
                            }

                        }
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
                        
                    default:
                        Console.WriteLine("Command not found, type 'help' to see commands.\n");
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


}