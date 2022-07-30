using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GreatestCommonDivisor
{
    static class GCDAlgorithms
    {
        /// <summary>
        /// Find the lowest common divisor of two numbers using Euclid's Algorithm
        /// see: http://en.wikipedia.org/wiki/Euclidean_algorithm
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Lowest common divisor</returns>
        static public int FindGCDEuclid(int a, int b)
        {
            // If the first number is zero, return the second
            if (a == 0) return b;

            // Calculate the LCD
            while (b != 0)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }

            return a;
        }

        // Add overloaded methods for 3,4, and 5 integers
        public static int FindGCDEuclid(int a, int b, int c)
        {
            int d = FindGCDEuclid(a, b);
            int e = FindGCDEuclid(d, c);
            return e;
        }

        public static int FindGCDEuclid(int a, int b, int c, int d)
        {
            int e = FindGCDEuclid(a, b, c);
            int f = FindGCDEuclid(e, d);
            return f;
        }

        public static int FindGCDEuclid(int a, int b, int c, int d, int e)
        {
            int f = FindGCDEuclid(a, b, c, d);
            int g = FindGCDEuclid(f, e);
            return g;
        }



    }
}
