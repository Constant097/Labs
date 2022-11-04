using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTree
{
    /// <summary>
    /// Interface that defines the basic functionality of a generic binary tree.
    /// </summary>
    /// <typeparam name="TItem">Type of item to store in the tree, must implement IComparable.</typeparam>
    public interface IBinaryTree<TItem> where TItem : IComparable<TItem>
    {
        /// <summary>
        /// Adds a new item to the tree.
        /// </summary>
        /// <param name="newItem">Item to add.</param>
        void Add(TItem newItem);

        /// <summary>
        /// Remove an item from the tree.
        /// <para>
        /// This method will search for an item with the same value in the tree, to remove.
        /// </para>
        /// <para>
        /// Nothing will happen if there is not a matching item in the tree.
        /// </para>
        /// </summary>
        /// <param name="itemToRemove">Item to remove.</param>
        void Remove(TItem itemToRemove);

        /// <summary>
        /// Displays the contents of the tree in order.
        /// </summary>
        void WalkTree();
    }
}
