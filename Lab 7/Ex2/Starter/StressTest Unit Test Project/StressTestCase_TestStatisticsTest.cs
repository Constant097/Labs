using StressTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StressTests_Unit_Test_Project
{
    
    
    /// <summary>
    ///This is a test class for StressTestCase_TestStatisticsTest and is intended
    ///to contain all StressTestCase_TestStatisticsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StressTestCase_TestStatisticsTest
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

        // TODO - Examine and run unit tests.
        /// <summary>
        ///A test for GetNumberOfFailures
        ///</summary>
        [TestMethod()]
        public void GetNumberOfFailuresTest()
        {
            TestStatistics target = new TestStatistics();
            target.IncrementTests(false);
            target.IncrementTests(false);
            int expected = 2; 
            int actual;
            actual = target.GetNumberOfFailures();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetNumberOfTestsPerformed
        ///</summary>
        [TestMethod()]
        public void GetNumberOfTestsPerformedTest()
        {
            TestStatistics target = new TestStatistics();
            target.IncrementTests(true);
            target.IncrementTests(false);
            target.IncrementTests(true);
            int expected = 3; 
            int actual;
            actual = target.GetNumberOfTestsPerformed();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IncrementTests
        ///</summary>
        [TestMethod()]
        public void IncrementTestsTest()
        {
            TestStatistics target = new TestStatistics();
            target.IncrementTests(true);
            target.IncrementTests(false);
            target.IncrementTests(true);
            target.IncrementTests(true);
            int expected = 4;
            int actual;
            actual = target.GetNumberOfTestsPerformed();
            Assert.AreEqual(expected, actual);
        }
    }
}
