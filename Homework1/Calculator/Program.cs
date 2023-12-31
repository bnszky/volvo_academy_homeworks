﻿using System.ComponentModel;

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

        // super additional subtask
        static void ProgramWithStringInput()
        {
            while (true)
            {
                string? aString, bString;
                Console.WriteLine("Please type mathematical formula as follow number1*number2. For factorial type number!");
                Console.WriteLine("For example type: -4,5/-2 (Warning! You can add and subtract only positive numbers in mathematical string input)");
                Console.WriteLine("You are allowed to add (+), subtract (-), multiply (*), divide (/), exponent (^), factorial (!)");
                Console.WriteLine("Type q to quit");

                // read input and parse to two strings
                string input = Console.ReadLine();
                int? operation = ConsoleManager.TryParseInput(input, out aString, out bString);

                // then validate and parse to double and int
                double a, b;
                int n;

                operation = ConsoleManager.ValidateInput(operation, aString, bString, out a, out b, out n);

                // here are all methods. I know it's really long but I'd like to represent it in a readable way.
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

        // randoms
        static void ProgramWithRandomData()
        {
            Random rnd = new Random();

            int a = -10; // minValue
            int b = 10; // maxValue
            Addition(rnd.Next(a, b + 1), rnd.Next(a, b + 1));
            Subtraction(rnd.Next(a, b + 1), rnd.Next(a, b + 1));
            Multiplication(rnd.Next(a, b + 1), rnd.Next(a, b + 1));
            Division(rnd.Next(a, b + 1), rnd.Next(a, b + 1));
            Exponentation(rnd.Next(a, b + 1), rnd.Next(a, b + 1));
            Factorial(rnd.Next(a, b + 1));

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // subtask 2
        static void ProgramWithOptions()
        {
            while (true)
            {
                int? operation = ConsoleManager.SelectOperation();

                double a, b;
                a = b = 0;

                // here are all methods. I know it's really long but I'd like to represent it in a readable way.
                switch (operation)
                {
                    case 1:
                        if (!ConsoleManager.ReadTwoNumbers(out a, out b)) break;
                        Addition(a, b);
                        break;
                    case 2:
                        if (!ConsoleManager.ReadTwoNumbers(out a, out b)) break;
                        Subtraction(a, b);
                        break;
                    case 3:
                        if (!ConsoleManager.ReadTwoNumbers(out a, out b)) break;
                        Multiplication(a, b);
                        break;
                    case 4:
                        if (!ConsoleManager.ReadTwoNumbers(out a, out b)) break;
                        Division(a, b);
                        break;
                    case 5:
                        if (!ConsoleManager.ReadOneNumber(out a)) break;
                        Factorial((int)a);
                        break;
                    case 6:
                        if (!ConsoleManager.ReadTwoNumbers(out a, out b)) break;
                        Exponentation(a, (int)b);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid option! Try again!");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Calculator 1.0 by Michal Banaszkiewicz");

            // Select version of project

            ProgramWithStringInput();
            //ProgramWithRandomData();
            //ProgramWithOptions();
        }
    }
}