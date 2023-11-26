using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal static class ConsoleManager
    {
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
