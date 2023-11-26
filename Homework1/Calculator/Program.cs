using System.ComponentModel;

namespace Calculator
{
    internal class Program
    {
        static void Addition(double a, double b)
        {
            double result = Arithmetic.AddTwoNumbers(a, b);
            Console.WriteLine($"{a} + {b} = {result}");
        } 
        static void Subtraction(double a, double b)
        {
            double result = Arithmetic.SubtractTwoNumbers(a, b);
            Console.WriteLine($"{a} - {b} = {result}");
        }
        static void Multiplication(double a, double b)
        {
            double result = Arithmetic.MultiplyTwoNumbers(a, b);
            Console.WriteLine($"{a} * {b} = {result}");
        }
        static void Division(double a, double b)
        {
            double? result = Arithmetic.DivideTwoNumbers(a, b);
            if(result != null) Console.WriteLine($"{a} / {b} = {result}");
        }
        static void Factorial(int n)
        {
            long? result = Arithmetic.FactorialFromN(n);
            if (result != null) Console.WriteLine($"{n}! = {result}");
        }
        static void Exponentation(double x, int n)
        {
            double? result = Arithmetic.Power(x, n);
            if (result != null) Console.WriteLine($"{x}^[{n}] = {result}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Calculator 1.0 by Michal Banaszkiewicz");
            while (true)
            {
                string? aString, bString;
                Console.WriteLine("Please type mathematical formula as follow number1*number2. For factorial type !number");
                Console.WriteLine("For example type: -4,5/-2");
                Console.WriteLine("You are allowed to add (+), subtract (-), multiply (*), divide (/), exponent (^), factorial (!)");
                Console.WriteLine("Enter q to quit");
                string input = Console.ReadLine();
                int? operation = ConsoleManager.TryParseInput(input, out aString, out bString);

                double a, b;
                int n;

                ConsoleManager.ValidateInput(operation, aString, bString, out a, out b, out n);

                switch (operation)
                {
                    case 1:
                        Addition(a, b);
                        break;
                    case 2:
                        Subtraction(a, b);
                        break;
                    case 3:
                        Multiplication(a, b);
                        break;
                    case 4:
                        Division(a, b);
                        break;
                    case 5:
                        Factorial(n);
                        break;
                    case 6:
                        Exponentation(a, n);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid input! Try again!");
                        break;
                }
            }
        }
    }
}