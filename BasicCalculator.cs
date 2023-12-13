using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PesOS
{
    //This method contains the function of a basic calculator for two variables
    public class BasicCalculator
    {
        public void calculator(string inputOperation)
        {
            for (decimal i = 1; i == 1; i = i)
            {
                decimal firstValue, secondValue;

                if (inputOperation != "+" && inputOperation != "-" && inputOperation != "*" && inputOperation != "/")
                {
                    Console.Clear(); 
                    Console.WriteLine("Invalid operation, PesOS Calculator Closed.");
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine("Welcome to PesOS");
                    Console.WriteLine("Type 'help' to view available commands");
                    break;
                }

                Console.WriteLine($"Input two values to be ({inputOperation}).");

                Console.Write("First Value: ");
                if (!decimal.TryParse(Console.ReadLine(), out firstValue))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                    continue;
                }

                Console.Write("Second Value: ");
                if (!decimal.TryParse(Console.ReadLine(), out secondValue))
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

                Console.WriteLine("\nWould you like to use PesOS calculator again? Type 'Yes' or 'No'.\n");

                for (decimal j = 1; j == 1; j = j)
                {
                    Console.Write("Input: ");
                    var reCalculate = Console.ReadLine().Trim().ToLower();

                    if (reCalculate == "No")
                    {
                        Console.Clear();
                        Console.WriteLine("PesOS Calculator Closed");
                        Console.WriteLine("--------------------------------------------------------\n");
                        Console.WriteLine("Welcome to PesOS");
                        Console.WriteLine("Type 'help' to view available commands");
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
    public class TaxCalculator
    {
        public void CalculateTax(string incomeStr)
        {
            if (string.IsNullOrWhiteSpace(incomeStr))
            {
                Console.WriteLine("Invalid input. Please enter a valid value.\n");
                return;
            }

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
                    decimal witholdingTax = income - result;
                    Console.WriteLine($"Your Witholding: {witholdingTax}");
                    Console.WriteLine($"Your Annual Net Income is: {result}");
                }
                else if (income > 400000 && income <= 800000)
                {
                    decimal result = income - (.20m * (income - 400000) + 22500);
                    decimal witholdingTax = income - result;
                    Console.WriteLine($"Your Withholding Tax: {witholdingTax}");
                    Console.WriteLine($"Your Annual Net Income is: {result}");
                }
                else if (income > 800000 && income <= 2000000)
                {
                    decimal result = income - (.25m * (income - 800000) + 102500);
                    decimal witholdingTax = income - result;
                    Console.WriteLine($"Your Witholding: {witholdingTax}");
                    Console.WriteLine($"Your Annual Net Income is: {result}");
                }
                else if (income > 2000000 && income <= 8000000)
                {
                    decimal result = income - (.30m * (income - 2000000) + 402500);
                    decimal witholdingTax = income - result;
                    Console.WriteLine($"Your Witholding: {witholdingTax}");
                    Console.WriteLine($"Your Annual Net Income is: {result}");
                }
                else if (income > 8000000)
                {
                    decimal result = income - (.35m * (income - 8000000) + 2202500);
                    decimal witholdingTax = income - result;
                    Console.WriteLine($"Your Witholding: {witholdingTax}");
                    Console.WriteLine($"Your Annual Net Income is: {result}");
                }
            }

            else if (incomeStr == "taxtable")
            {
                taxTable();
            }

            else if (incomeStr == "taxterms")
            {
                taxTerminologies();
            }

            else
            {
                Console.WriteLine("Invalid input. Please enter a valid value.\n");
            }

        }

        public void taxTable()
        {
            Console.WriteLine("\nIncome Tax Rates");
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
            Console.WriteLine("Department of Finance - Tax Schedule Effective January 1, 2023 and onwards\n");
        }

        public void taxTerminologies()
        {
            Console.WriteLine("\n\nAs per the Republic of the Philippines - Bureau of Internal Revenue (BIR), Philippine Statistics Authority (PSA), and Business News Daily, the following terms shall be defined as:");
            Console.WriteLine("\nStrata - It is the division of population with common characteristics, such as range of annual gross income. ");
            Console.WriteLine("Annual Gross Income - Regardless of source, Gross Income is the total income for the whole year.");
            Console.WriteLine("Annual Net Income - Annual Net Income is the amount  earned for the whole year, having Withholding Tax deducted. ");
            Console.WriteLine("Withholding Tax - It is the tax being deducted from employee, income payments, and Government managements.\n");
        }
    }
}
