using System.ComponentModel;

namespace Calculator
{
    internal class Program
    {
        static void Addition()
        {
            double a, b;
            if(!ConsoleManager.ReadTwoNumbers(out a, out b)) return;

            double result = Arithmetic.AddTwoNumbers(a, b);
            Console.WriteLine($"{a} + {b} = {result}");
        } 
        static void Subtraction()
        {
            double a, b;
            if (!ConsoleManager.ReadTwoNumbers(out a, out b)) return;

            double result = Arithmetic.SubtractTwoNumbers(a, b);
            Console.WriteLine($"{a} - {b} = {result}");
        }
        static void Multiplication()
        {
            double a, b;
            if (!ConsoleManager.ReadTwoNumbers(out a, out b)) return;

            double result = Arithmetic.MultiplyTwoNumbers(a, b);
            Console.WriteLine($"{a} * {b} = {result}");
        }
        static void Division()
        {
            double a, b;
            if (!ConsoleManager.ReadTwoNumbers(out a, out b)) return;

            double? result = Arithmetic.DivideTwoNumbers(a, b);
            if(result != null) Console.WriteLine($"{a} / {b} = {result}");
        }
        static void Factorial()
        {
            int n;
            Console.WriteLine("Please type number for factorial (only non-negative integer): ");
            if (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Your input is incorrect!");
                return;
            }

            long? result = Arithmetic.FactorialFromN(n);
            if (result != null) Console.WriteLine($"{n}! = {result}");
        }
        static void Exponentation()
        {
            double x; int n;
            Console.WriteLine("Please type base: ");
            bool isXDouble = double.TryParse(Console.ReadLine(), out x);
            Console.WriteLine("Please type exponent (only integer): ");
            bool isNInt = int.TryParse(Console.ReadLine(), out n);

            if (!isXDouble || !isNInt)
            {
                Console.WriteLine("Your input is incorrect!");
                return;
            }

            double? result = Arithmetic.Power(x, n);
            if (result != null) Console.WriteLine($"{x}^[{n}] = {result}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Calculator 1.0 by Michal Banaszkiewicz");
            while (true)
            {
                int? selected = ConsoleManager.SelectOperation();
                switch (selected)
                {
                    case 1:
                        Addition();
                        break;
                    case 2:
                        Subtraction();
                        break;
                    case 3:
                        Multiplication();
                        break;
                    case 4:
                        Division();
                        break;
                    case 5:
                        Factorial();
                        break;
                    case 6:
                        Exponentation();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid selection! Try again!");
                        break;
                }

                Console.WriteLine("Please press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}