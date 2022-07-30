using StressTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StressTests_Unit_Test_Project
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


        /// <summary>
        ///A test for PerformStressTest
        ///</summary>
        [TestMethod()]
        public void PerformStressTestTest()
        {
            StressTestCase target = new StressTestCase();
            target.PerformStressTest();
            TestCaseResult tcr = (TestCaseResult)target.GetStressTestResult();
            Assert.IsNotNull(tcr);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string expected = "Material: StainlessSteel, CrossSection: IBeam, Length: 4000mm, Height: 20mm, Width: 15mm, No Stress Test Performed";
            string actual;
            StressTestCase target = new StressTestCase();
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetStressTestResult
        ///</summary>
        [TestMethod()]
        public void GetStressTestResultTest1()
        {
            StressTestCase target = new StressTestCase();
            object actual;
            actual = target.GetStressTestResult();
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetStressTestResult when a test has been performed
        ///</summary>
        [TestMethod()]
        public void GetStressTestResultTest2()
        {
            StressTestCase target = new StressTestCase();
            TestCaseResult actual;
            target.PerformStressTest();
            actual = (TestCaseResult)target.GetStressTestResult();
            Assert.IsNotNull(actual);
        }
 
       
        /// <summary>
        ///A test for StressTestCase Constructor
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
            string actual;
            StressTestCase target = new StressTestCase(girderMaterial, crossSection, lengthInMm, heightInMm, widthInMm);
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StressTestCase Constructor
        ///</summary>
        [TestMethod()]
        public void StressTestCaseConstructorTest1()
        {
            string expected = "Material: StainlessSteel, CrossSection: IBeam, Length: 4000mm, Height: 20mm, Width: 15mm, No Stress Test Performed";
            string actual;
            StressTestCase target = new StressTestCase();
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
