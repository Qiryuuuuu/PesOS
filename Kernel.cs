using Cosmos.Core;
using Cosmos.HAL;
using System;
using System.Data;
using System.Threading;
using Sys = Cosmos.System;

namespace PesOS
{
    public class Kernel : Sys.Kernel
    {// Display a centered title with a specified delay between characters
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
            Thread.Sleep(2000); //This is the delay before the titles fade out
            Console.Clear();
        }

        // This method is called before the main execution starts
        protected override void BeforeRun()
        {
            ShowCenteredTitle("Innovation is the new Currency", 150);
            ShowCenteredTitle("PesOS", 400);
            Console.WriteLine("PesOS booted successfully. Type a line of text to get it echoed back.");
        }

        // This method contains the main execution logic
        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);

            Date checkDate = new Date();
            Console.WriteLine("DATE and TIME");
            checkDate.outputDate();

            Date checkTime = new Date();
            checkTime.outputTime();
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
}

