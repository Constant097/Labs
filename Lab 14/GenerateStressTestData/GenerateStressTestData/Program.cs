using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using StressTestResult;

namespace GenerateStressTestData
{
    class Program
    {
        private const string stressDataFilename = @"E:\Labfiles\Lab 14\StressData.dat";

        static void Main(string[] args)
        {
            using (FileStream writeStream = File.Open(stressDataFilename, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                for (DateTime resultsDate = DateTime.Now.AddMonths(-10); resultsDate <= DateTime.Now.AddMonths(6); resultsDate = resultsDate.AddDays(new Random().Next(3, 10)))
                {
                    for (short resultsTemperature = 200; resultsTemperature <= 500; resultsTemperature += (short)(resultsTemperature < 350 ? 10 : 50))
                    {
                        short deflection = (short)(new Random().Next(0, 5));
                        resultsDate = resultsDate.AddMinutes(3);

                        for (short resultsStress = 10; resultsStress <= 5000; resultsStress+= (short)(resultsStress < 2000 ? 10 : 25))
                        {                                                      
                            Console.WriteLine("{0}, {1}, {2}, {3}", resultsDate, resultsTemperature, resultsStress, deflection);
                            TestResult data = new TestResult { TestDate = resultsDate, Temperature = resultsTemperature, AppliedStress = resultsStress, Deflection = deflection };
                            formatter.Serialize(writeStream, data);

                            deflection += (short)new Random().Next(0, 100);
                            if (deflection > 1500)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            
        }
    }
}
