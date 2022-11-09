using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinaryTree;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO - Modify the test harness to use the BuildTree method.
            IBinaryTree<int> tree = new Tree<int>(5);
            tree.Add(1);
            tree.Add(4);
            tree.Add(7);
            tree.Add(3);
            tree.Add(4);
            Console.WriteLine("Current Tree: ");
            tree.WalkTree();
            Console.WriteLine("Add 15");
            tree.Add(15);
            Console.WriteLine("Current Tree: ");
            tree.WalkTree();
            Console.WriteLine("Remove 5");
            tree.Remove(5);
            Console.WriteLine("Current Tree: ");
            tree.WalkTree();
            Console.ReadLine();
        }
    }
}
