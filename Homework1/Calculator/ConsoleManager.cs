using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal static class ConsoleManager
    {
        private static bool SplitIfSignExist(string text, char c, out string? firstNumber, out string? secondNumber)
        {
            int index = text.IndexOf(c);
            if (index == -1)
            {
                firstNumber = null;
                secondNumber = null;
                return false;
            }
            else
            {
                firstNumber = text.Substring(0, index);
                secondNumber = text.Substring(index + 1);
                return true;
            }
        }
        public static int? TryParseInput(string text, out string? firstNumber, out string? secondNumber)
        {
            firstNumber = null;
            secondNumber = null;
            if (text.ToLower() == "q") return 0;

            char[] signs = { '+', '-', '*', '/', '!', '^' };
            for(int i = 0; i < signs.Length; i++)
            {
                if (SplitIfSignExist(text, signs[i], out firstNumber, out secondNumber))
                {
                    return i + 1;
                }
            }

            return null;
        }

        public static int? ValidateInput(int? operation, string aString, string bString, out double a, out double b, out int n)
        {
            a = 0; b = 0; n = 0;
            // if isn't possible to parse string set operation to null 
            if (operation >= 1 && operation <= 4)
            {
                if (!double.TryParse(aString, out a)) operation = null;
                if (!double.TryParse(bString, out b)) operation = null;
            }
            else if (operation == 5)
            {
                if (aString.Length > 0) operation = null;
                if (!int.TryParse(bString, out n)) operation = null;
            }
            else if (operation == 6)
            {
                if (!double.TryParse(aString, out a)) operation = null;
                if (!int.TryParse(bString, out n)) operation = null;
            }

            return operation;
        }
        public static int? SelectOperation()
        {
            Console.WriteLine("Please select operation by typing corresponding number:");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Factorial");
            Console.WriteLine("6. Exponentiation");
            Console.WriteLine("7. Exit");
            string input = Console.ReadLine();
            int number;
            return (int.TryParse(input, out number) && number >= 1 && number <= 7) ? number : null;
        }

        public static bool ReadTwoNumbers(out double a, out double b)
        {
            Console.WriteLine("Please type first number: ");
            bool isADouble = double.TryParse(Console.ReadLine(), out a);
            Console.WriteLine("Please type second number: ");
            bool isBDouble = double.TryParse(Console.ReadLine(), out b);

            if (!isADouble || !isBDouble)
            {
                Console.WriteLine("Your input is incorrect!");
                return false;
            }

            return true;
        }
    }
}
