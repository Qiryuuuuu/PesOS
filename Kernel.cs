using System;
using System.Threading;
using Sys = Cosmos.System;

namespace pesoOS
{
    public class Kernel : Sys.Kernel
    {
        // Display a centered title with a specified delay between characters
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
        }
    }
}
