using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesOS
{
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
