using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;        
            while ((line = Console.ReadLine()) != null)
            {
                line = line.Replace(",", " y:");
                line = "x:" + line;
                Console.WriteLine(line);
            }

        }
    }
}

