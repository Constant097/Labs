using StressTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Module_7_Unit_Test_Project
{
    
    
    /// <summary>
    ///This is a test class for StressTestCaseTest and is intended
    ///to contain all StressTestCaseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StressTestCaseTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        // TODO - Update unit tests to resolve errors.
        // Modify the constructor unit tests so that they don't rely on access to the private fields.
        /// <summary>
        ///A test for StressTestCase parameterized Constructor
        ///</summary>
        [TestMethod()]
        public void StressTestCaseConstructorTest()
        {
            
            Material girderMaterial = Material.Composite;
            CrossSection crossSection = CrossSection.CShaped;
            int lengthInMm = 5000;
            int heightInMm = 32;
            int widthInMm = 18;
string expected = "Material: Composite, CrossSection: CShaped, Length: 5000mm, Height: 32mm, Width: 18mm, No Stress Test Performed";
             StressTestCase target = new StressTestCase(
                 girderMaterial,
                crossSection,
                lengthInMm,
                heightInMm,
                widthInMm);
            StressTestCase target = new StressTestCase(girderMaterial, crossSection, lengthInMm, heightInMm, widthInMm);
           /* Assert.AreEqual(Material.Composite, target.girderMaterial);
            Assert.AreEqual(CrossSection.CShaped, target.crossSection);
            Assert.AreEqual(5000, target.lengthInMm);
            Assert.AreEqual(32, target.heightInMm);
            Assert.AreEqual(18, target.widthInMm);*/
        }

        /// <summary>
        ///A test for StressTestCase default Constructor
        ///</summary>
        [TestMethod()]
        public void StressTestCaseConstructorTest1()
        {
             string expected = "Material: StainlessSteel, CrossSection: IBeam, Length: 4000mm, Height: 20mm, Width: 15mm, No Stress Test Performed";
            StressTestCase target = new StressTestCase();
           /* Assert.AreEqual(Material.StainlessSteel, target.girderMaterial);
            Assert.AreEqual(CrossSection.IBeam, target.crossSection);
            Assert.AreEqual(4000, target.lengthInMm);
            Assert.AreEqual(20, target.heightInMm);
            Assert.AreEqual(15, target.widthInMm);*/
            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for GetStressTestResult
        ///</summary>
        [TestMethod()]
        public void GetStressTestResultTest()
        {
            StressTestCase target = new StressTestCase();
            Assert.IsFalse(target.GetStressTestResult().HasValue);
            target.PerformStressTest();
            Assert.IsTrue(target.GetStressTestResult().HasValue);
        }

        /// <summary>
        ///A test for PerformStressTest
        ///</summary>
        [TestMethod()]
        public void PerformStressTestTest()
        {
            for (int i = 0; i < 30; i++)
            {
                StressTestCase target = new StressTestCase();
                target.PerformStressTest();
                Assert.IsTrue(target.GetStressTestResult().HasValue);
                TestCaseResult actual = target.GetStressTestResult().Value;
                if (actual.result == TestResult.Fail)
                    Assert.IsTrue(actual.reasonForFailure.Length > 0);
                else
                    Assert.IsTrue(actual.reasonForFailure == null);
            }
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            StressTestCase target = new StressTestCase();
            string expected = "Material: StainlessSteel, CrossSection: IBeam, Length: 4000mm, Height: 20mm, Width: 15mm, No Stress Test Performed";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
