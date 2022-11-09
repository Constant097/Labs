
using BinaryTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_for_Module12
{
    
    
    /// <summary>
    ///This is a test class for TreeTest and is intended
    ///to contain all TreeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TreeTest
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
        ///A test for Tree Constructor
        ///</summary>
        public void TreeConstructorTestHelper<TItem>(TItem nodeValue)
            where TItem : IComparable<TItem>
        {
            Tree<TItem> target = new Tree<TItem>(nodeValue);
            Assert.AreEqual(target.NodeData, nodeValue);
        }

        [TestMethod()]
        public void TreeConstructorTest()
        {
            TreeConstructorTestHelper<int>(4);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        public void AddTestHelper<TItem>(TItem nodeValue, TItem newItem)
            where TItem : IComparable<TItem>
        {
            Tree<TItem> target = new Tree<TItem>(nodeValue); 
            target.Add(newItem);
            Assert.IsNotNull(target.RightTree);
        }

        [TestMethod()]
        public void AddTest()
        {
            AddTestHelper<int>(3, 4);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Tree<int> tree = new Tree<int>(5);
            tree.Add(4);
            tree.Add(6);
            tree.Add(7);
            tree.Add(9);
            tree.Add(8);
            tree.Add(10);
            tree.Add(4);
            tree.Add(1);
            tree.Add(3);
            tree.Add(9);
            tree.Add(15);
            tree.Remove(6);
            tree.Remove(8);
            tree.Remove(4);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void WalkTreeTest()
        {
            Tree<int> target = new Tree<int>(4);
            target.Add(1);
            target.Add(3);
            target.WalkTree();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for NodeData
        ///</summary>
        public void NodeDataTestHelper<TItem>(TItem nodeValue)
            where TItem : IComparable<TItem>
        {
            Tree<TItem> target = new Tree<TItem>(nodeValue);
            TItem expected = nodeValue; 
            TItem actual = target.NodeData;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NodeDataTest()
        {
            NodeDataTestHelper<int>(5);
        }

        [TestMethod()]
        public void BuildTreeTest()
        {
            Tree<int> tree = Tree<int>.BuildTree<int>(4, new int[] {4, 6, 7, 9, 1, 6, 10});
            Assert.IsNotNull(tree);
        }
    }
}
