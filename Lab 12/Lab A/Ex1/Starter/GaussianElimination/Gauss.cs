using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GaussianElimination
{
    public static class Gauss
    {
        static Hashtable results;
        /// <summary>
        /// Number of equations (and variables) to solve for
        /// </summary>
        public const int numberOfEquations = 4;

        /// <summary>
        /// Solve simultaneous equations using Gaussian method
        /// </summary>
        /// <param name="coefficients">Coefficients from all equations</param>
        /// <param name="rhs">Constants from all equations</param>
        /// <returns>Array of solution results</returns>
        public static double[] SolveGaussian(double[,] coefficients, double[] rhs)
        {
            if (results == null)
            {
                results = new Hashtable();
            }
            StringBuilder hashString = new StringBuilder();
            foreach (double coefficient in coefficients)
            {
                hashString.Append(coefficient);
            }
            foreach (double value in rhs)
            {
                hashString.Append(value);
            }
            string hashValue = hashString.ToString();
            if (results.Contains(hashValue))
            {
                // If the result has been calculated previously return the result 
                // from the hashtable.
                return (double[])results[hashValue];
            }
            else
            // If it has not been solved previously calculate the result.
            // After calculating the result, add it to the hashtable.
            {
                // Make a deep copy of the parameters
                double[,] coefficientsCopy = DeepCopy2D(coefficients);
                double[] rhsCopy = DeepCopy1D(rhs);

                double x, sum;

                //convert to upper triangular form
                for (int k = 0; k < numberOfEquations - 1; k++)
                {
                    try
                    {
                        for (int i = k + 1; i < numberOfEquations; i++)
                        {
                            x = coefficientsCopy[i, k] / coefficientsCopy[k, k];
                            for (int j = k + 1; j < numberOfEquations; j++)
                                coefficientsCopy[i, j] = coefficientsCopy[i, j] - coefficientsCopy[k, j] * x;

                            rhsCopy[i] = rhsCopy[i] - rhsCopy[k] * x;
                        }
                    }
                    catch (DivideByZeroException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                // back substitution
                rhsCopy[numberOfEquations - 1] = rhsCopy[numberOfEquations - 1] / coefficientsCopy[numberOfEquations - 1, numberOfEquations - 1];
                for (int i = numberOfEquations - 2; i >= 0; i--)
                {
                    sum = rhsCopy[i];
                    for (int j = i + 1; j < numberOfEquations; j++)
                        sum = sum - coefficientsCopy[i, j] * rhsCopy[j];
                    rhsCopy[i] = sum / coefficientsCopy[i, i];
                }
                // Pause to exaggerate the benefit of implementing caching.
                // In larger applications the gain could be significantly greater.
                System.Threading.Thread.Sleep(5000);
                
                results.Add(hashValue, rhsCopy);
                return rhsCopy;
            }
        }

        /// <summary>
        /// Deep copy a one dimensional array
        /// </summary>
        /// <param name="array">Array to copy</param>
        /// <returns>New Array</returns>
        private static double[] DeepCopy1D(double[] array)
        {
            // Get dimensions
            int columns = array.GetLength(0);

            // Initialize a new array
            double[] newArray = new double[columns];

            // Copy the values
            for (int i = 0; i < columns; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

        /// <summary>
        /// Deep copy a two dimensional array
        /// </summary>
        /// <param name="array">Array to copy</param>
        /// <returns>New Array</returns>
        private static double[,] DeepCopy2D(double[,] array)
        {
            // Get dimensions
            int columns = array.GetLength(0);
            int rows = array.GetLength(1);

            // Initialize a new array
            double[,] newArray = new double[columns, rows];

            // Copy the values
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    newArray[i, j] = array[i, j];
                }
            }
            return newArray;
        }
    }
}
