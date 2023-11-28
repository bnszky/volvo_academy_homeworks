using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal static class Arithmetic
    {
        public static double AddTwoNumbers(double a, double b) { return Math.Round(a + b, 10); }
        public static double SubtractTwoNumbers(double a, double b) { return Math.Round(a - b, 10); }
        public static double MultiplyTwoNumbers(double a, double b) { return Math.Round(a * b, 10); }
        public static double? DivideTwoNumbers(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("You can't divide by zero!");
                return null;
            }
            return Math.Round(a / b, 10);
        }
        public static long? FactorialFromN(int n)
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
        /*
        private static double SlowerPower(double x, int n)
        {
            double power = 1;
            while (n-- > 0)
            {
                power *= x;
            }
            return power;
        }
        */

        // Smarter way of exponentation using divide and conquer technique
        private static double SmarterPower(double x, int n)
        {
            if (n == 0) return 1;
            double power = SmarterPower(x, n / 2);
            power *= power;
            if (n % 2 == 1) power *= x;
            return power;
        }
        public static double? Power(double x, int n)
        {
            if (n < 0 && x == 0)
            {
                Console.WriteLine("0 to negative exponent is undefined!");
                return null;
            }

            double result = SmarterPower(x, Math.Abs(n));
            if (n < 0) return Math.Round((double)1 / result, 10);
            return result;
        }
    }
}
