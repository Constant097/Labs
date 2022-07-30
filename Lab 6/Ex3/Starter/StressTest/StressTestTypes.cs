using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StressTest
{
    // Enumerations Exercise 1

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

    // Structures Exercise 2

    /// <summary>
    /// Structure containing test results
    /// </summary>
    public struct TestCaseResult
    {
        /// <summary>
        /// Test result (enumeration type)
        /// </summary>
        public TestResult Result;

        /// <summary>
        /// Description of reason for failure
        /// </summary>
        public string ReasonForFailure;
    }

    public class StressTestCase
    {
        public Material GirderMaterial;
        public CrossSection CrossSection;
        public int LengthInMm;
        public int HeightInMm;
        public int WidthInMm;
        public TestCaseResult TestCaseResult;
        public StressTestCase() : this(Material.StainlessSteel, CrossSection.IBeam, 4000, 20, 15) { }
        public StressTestCase(Material girderMaterial, CrossSection crossSection, int lengthInMm, int heightInMm, int widthInMm)
        {
            this.GirderMaterial = girderMaterial;
            this.CrossSection = crossSection;
            this.LengthInMm = lengthInMm;
            this.HeightInMm = heightInMm;
            this.WidthInMm = widthInMm;
        }
        public void PerformStressTest()
        {
           string[] failureReasons = { "Fracture detected", "Beam snapped", "Beam dimensions wrong", "Beam warped", "Other" };
            if (Utility.Rand.Next(10) == 9)
            {
                TestCaseResult.Result = TestResult.Fail;
                int failureCode = Utility.Rand.Next(5);
                TestCaseResult.ReasonForFailure = failureReasons[failureCode];
            }
            else
            {
                TestCaseResult.Result = TestResult.Pass;
            }
        }
        public TestCaseResult GetStressTestResult()
        {
            return TestCaseResult;
        }
        public override string ToString()
        {
            return string.Format("Material: {0}, CrossSection: {1}, Length: {2}mm, Height: {3}mm, Width: {4}mm",
                GirderMaterial.ToString(),
                CrossSection.ToString(),
                LengthInMm,
                HeightInMm,
                WidthInMm);
        }
    }
}
