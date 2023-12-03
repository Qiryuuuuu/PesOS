using Cosmos.Core;
using Cosmos.HAL;
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
                Console.WriteLine("PesOS requires authentication.");
                AuthenticateUser();
            }

            Console.WriteLine($"Welcome, {currentUser.Username}!");
            Console.WriteLine($"Host Identity Code: {currentUser.HostIdentityCode}");

            Console.WriteLine("PesOS booted successfully. Type a line of text to get it echoed back.");
            Console.WriteLine("Type 'restart' to restart the system or 'shutdown' to shut it down.");
        }

        // This method contains the main execution logic
        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);

            if (input.ToLower() == "restart")
            {
                RestartSystem();
            }
            else if (input.ToLower() == "shutdown")
            {
                ShutdownSystem();
            }
            else
            {
                Date checkDate = new Date();
                Console.WriteLine("DATE and TIME");
                checkDate.outputDate();

                Date checkTime = new Date();
                checkTime.outputTime();
            }
        }

        private void AuthenticateUser()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            currentUser = users.Find(u => u.Username == username && u.ValidatePassword(password));

            if (currentUser == null)
            {
                Console.WriteLine("Authentication failed. User not found or invalid credentials. Try again.");
            }
        }

        private void RestartSystem()
        {
            Console.WriteLine("Restarting the system...");
            // Additional logic for restarting the system can be added here
            Sys.Power.Reboot();
        }

        private void ShutdownSystem()
        {
            Console.WriteLine("Shutting down the system...");
            // Additional logic for shutting down the system can be added here
            Sys.Power.Shutdown();
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

public class Date
{
    public static string CurrentDate(bool DisplayWeekday, bool DisplayYear)
    {
        string Weekday = "";
        string Month = "";
        if (DisplayWeekday)
        {
            switch (RTC.DayOfTheWeek)
            {
                case 1:
                    Weekday = "Monday, ";
                    break;
                case 2:
                    Weekday = "Tuesday, ";
                    break;
                case 3:
                    Weekday = "Wednesday, ";
                    break;
                case 4:
                    Weekday = "Thursday, ";
                    break;
                case 5:
                    Weekday = "Friday, ";
                    break;
                case 6:
                    Weekday = "Saturday, ";
                    break;
                case 7:
                    Weekday = "Sunday, ";
                    break;
            }
        }
        switch (RTC.Month)
        {
            case 1:
                Month = "January";
                break;
            case 2:
                Month = "February";
                break;
            case 3:
                Month = "March";
                break;
            case 4:
                Month = "April";
                break;
            case 5:
                Month = "May";
                break;
            case 6:
                Month = "June";
                break;
            case 7:
                Month = "July";
                break;
            case 8:
                Month = "August";
                break;
            case 9:
                Month = "September";
                break;
            case 10:
                Month = "October";
                break;
            case 11:
                Month = "November";
                break;
            case 12:
                Month = "December";
                break;

        }
        if (DisplayYear)
        {
            if (DisplayWeekday)
            {
                return Weekday + RTC.DayOfTheMonth + "." + RTC.Month + ".20" + RTC.Year;
            }
            else
            {
                return RTC.DayOfTheMonth + "." + RTC.Month + ".20" + RTC.Year;
            }
        }
        else
        {
            if (DisplayWeekday)
            {
                return Weekday + RTC.DayOfTheMonth + " " + Month;
            }
            else
            {
                return RTC.DayOfTheMonth + " " + Month;
            }
        }

    }
    public void outputDate()
    {
        Console.Write("Date:" + RTC.Month + "." + RTC.DayOfTheMonth + ".20" + RTC.Year + " ");
    }
    public static string CurrentTime(bool DisplaySeconds)
    {
        string hour = RTC.Hour.ToString();
        if (hour.Length == 1) { hour = "0" + hour; }
        string min = RTC.Minute.ToString();
        if (min.Length == 1) { min = "0" + min; }
        if (DisplaySeconds)
        {
            string sec = RTC.Second.ToString();
            if (sec.Length == 1) { sec = "0" + sec; }
            return hour + ":" + min + ":" + sec;
        }
        else
        {
            return hour + ":" + min;
        }
    }
    public static string CurrentSecond()
    {
        string sec = RTC.Second.ToString();
        if (sec.Length == 1) { sec = "0" + sec; }
        return sec;
    }
    public void outputTime()
    {
        Console.Write("Time:" + RTC.Hour.ToString() + ":" + RTC.Minute.ToString() + ":" + RTC.Second.ToString() + "\n");
    }
}
