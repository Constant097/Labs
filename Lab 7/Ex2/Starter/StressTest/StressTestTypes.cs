using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StressTest
{
    /// <summary>
    /// Enumeration of girder material types
    /// </summary>
    public enum Material { StainlessSteel, Aluminium, ReinforcedConcrete, Composite, Titanium }

    /// <summary>
    /// Enumeration of girder cross-sections
    /// </summary>
    public enum CrossSection { IBeam, Box, ZShaped, CShaped }

    /// <summary>
    /// Enumeration of test results
    /// </summary>
    public enum TestResult { Pass, Fail }

    /// <summary>
    /// Structure containing test results
    /// </summary>
    public struct TestCaseResult
    {
        /// <summary>
        /// Test result (enumeration type)
        /// </summary>
        public TestResult result;

        /// <summary>
        /// Description of reason for failure
        /// </summary>
        public string reasonForFailure;
    }

    /// <summary>
    /// Defines details of a complete girder stress test
    /// </summary>
    public class StressTestCase
    {
        /// <summary>
        /// Girder material type (enumeration type)
        /// </summary>
        private Material girderMaterial;

        /// <summary>
        /// Girder cross-section (enumeration type)
        /// </summary>
        private CrossSection crossSection;

        /// <summary>
        /// Girder length in millimeters
        /// </summary>
        private int lengthInMm;

        /// <summary>
        /// Girder height in millimeters
        /// </summary>
        private int heightInMm;

        /// <summary>
        /// Girder width in millimeters
        /// </summary>
        private int widthInMm;

        /// <summary>
        /// Details of test result (structure type)
        /// Made nullable
        /// </summary>
        private TestCaseResult? testCaseResult;
        private static TestStatistics statistics;
        public static TestStatistics GetStatistics()
        {
            return statistics;
        }
        public static void ResetStatistics()
        {
            statistics.ResetCounters();
        }
        public StressTestCase() : this(Material.StainlessSteel, CrossSection.IBeam, 4000, 20, 15) { }
        
        // Add a private static field of type TestStatistics and a public get method

        // Add a static method to reset the statistics to zero

        /// <summary>
        /// No argument constructor (invokes parameterised constructor passing default values)
        /// </summary>
        /// <summary>
        /// Constructor - initializes testCaseResult to null
        /// </summary>
        /// <param name="girderMaterial">Girder material type (enumeration type)</param>
        /// <param name="crossSection">Girder cross-secion type (enumeration type)</param>
        /// <param name="lengthInMm">Girder length in millimeters</param>
        /// <param name="heightInMm">Girder height in millimeters</param>
        /// <param name="widthInMm">Girder width in millimeters</param>
        public StressTestCase(Material girderMaterial, CrossSection crossSection, int lengthInMm, int heightInMm, int widthInMm)
        {
            this.girderMaterial = girderMaterial;
            this.crossSection = crossSection;
            this.lengthInMm = lengthInMm;
            this.heightInMm = heightInMm;
            this.widthInMm = widthInMm;
            this.testCaseResult = null;
        }

        /// <summary>
        /// Execute a stress test and save the results in the testCaseResult field
        /// </summary>
        public void PerformStressTest()
        {
            // Create a new TestCaseResult
            TestCaseResult tcr = new TestCaseResult();

            // List of possible reasons for a failure
            string[] failureReasons = { "Fracture detected", "Beam snapped", "Beam dimensions wrong", "Beam warped", "Other" };

            // Fails 1 time in 10
            if (Utility.Rand.Next(10) == 9)
            {
                tcr.result = TestResult.Fail;
                tcr.reasonForFailure = failureReasons[Utility.Rand.Next(5)];
                statistics.IncrementTests(false);
                // Update the statistics
            }
            else
            {
                tcr.result = TestResult.Pass;
                statistics.IncrementTests(true);
                // Update the statistics
            }
            testCaseResult = tcr;
        }

        /// <summary>
        /// Return the results of the test
        /// Could return null
        /// </summary>
        /// <returns>Results of test</returns>
        public TestCaseResult? GetStressTestResult()
        {
            if (testCaseResult.HasValue)
            {
                return testCaseResult.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Override of ToString
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            string stressTestPerformed;
            if (testCaseResult.HasValue)
            {
                stressTestPerformed = "Stress Test Completed";
            }
            else
            {
                stressTestPerformed = "No Stress Test Performed";
            }
            return string.Format("Material: {0}, CrossSection: {1}, Length: {2}mm, Height: {3}mm, Width: {4}mm, {5}",
                girderMaterial.ToString(), crossSection.ToString(), lengthInMm, heightInMm, widthInMm, stressTestPerformed);
        }


    }
    public struct TestStatistics
    {
        private int numberOfTestsPerformed;
        private int numberOfFailures;
        public void IncrementTests(bool success)
        {

            numberOfTestsPerformed++;
            if (!success)
            {
                numberOfFailures++;
            }
        }
        public int GetNumberOfTestsPerformed()
        {
            return numberOfTestsPerformed;
        }
        public int GetNumberOfFailures()
        {
            return numberOfFailures;
        }
        internal void ResetCounters()
        {
            numberOfFailures = 0;
            numberOfTestsPerformed = 0;
        }
    }
}
