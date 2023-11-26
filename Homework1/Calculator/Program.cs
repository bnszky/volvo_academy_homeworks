using System.ComponentModel;

namespace Calculator
{
    internal class Program
    {
        static int? SelectOperation()
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

        static bool ReadTwoNumbers(out double a, out double b)
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

        static double AddTwoNumbers(double a, double b) { return a + b; }
        static double SubtractTwoNumbers(double a, double b) { return a - b; }
        static double MultiplyTwoNumbers(double a, double b) { return a * b; }
        static double? DivideTwoNumbers(double a, double b) {
            if (b == 0)
            {
                Console.WriteLine("You can't divide by zero!");
                return null;
            }
            return a / b;
        }
        static long? FactorialFromN(int n)
        {
            if (n < 0)
            {
                Console.WriteLine("Factorial of negative integer doesn't exist!");
                return null;
            }
            if (n == 0) return 1;
            return FactorialFromN(n - 1) * n;
        }

        // Very intuitive implementation of exponentation
        static double SlowerPower(double x, int n)
        {
            double power = 1;
            while (n-- > 0)
            {
                power *= x;
            }
            return power;
        }
        // Smarter way of exponentation using divide and conquer technique
        static double SmarterPower(double x, int n)
        {
            if (n == 0) return 1;
            double power = SmarterPower(x, n / 2);
            power *= power;
            if (n % 2 == 1) power *= x;
            return power;
        }
        static double? Power(double x, int n)
        {
            if (n < 0 && x == 0)
            {
                Console.WriteLine("0 to negative exponent is undefined!");
                return null;
            }

            double result = SmarterPower(x, Math.Abs(n));
            if (n < 0) return ((double)1 / result);
            return result;
        }
        static void Addition()
        {
            double a, b;
            if(!ReadTwoNumbers(out a, out b)) return;

            double result = AddTwoNumbers(a, b);
            Console.WriteLine($"{a} + {b} = {result}");
        } 
        static void Subtraction()
        {
            double a, b;
            if (!ReadTwoNumbers(out a, out b)) return;

            double result = SubtractTwoNumbers(a, b);
            Console.WriteLine($"{a} - {b} = {result}");
        }
        static void Multiplication()
        {
            double a, b;
            if (!ReadTwoNumbers(out a, out b)) return;

            double result = MultiplyTwoNumbers(a, b);
            Console.WriteLine($"{a} * {b} = {result}");
        }
        static void Division()
        {
            double a, b;
            if (!ReadTwoNumbers(out a, out b)) return;

            double? result = DivideTwoNumbers(a, b);
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

            long? result = FactorialFromN(n);
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

            double? result = Power(x, n);
            if (result != null) Console.WriteLine($"{x}^[{n}] = {result}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Calculator 1.0 by Michal Banaszkiewicz");
            while (true)
            {
                int? selected = SelectOperation();
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