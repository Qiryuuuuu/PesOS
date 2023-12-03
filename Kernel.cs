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
                        Console.WriteLine("These are the available commands of PesOS");
                        Console.WriteLine("help - display the available commands");
                        Console.WriteLine("info - display information of the Operating system");
                        Console.WriteLine("clock - display the current date and time");
                        Console.WriteLine("clear - clear the console");
                        Console.WriteLine("restart - automatically restarts the operating system");
                        Console.WriteLine("shutdown - automatically shutdowns the operating system");
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
