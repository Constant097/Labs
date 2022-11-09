using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTree
{
    
    public interface IBinaryTree<TItem> where TItem : IComparable<TItem>
    {
        
        void Add(TItem newItem);

        
        void Remove(TItem itemToRemove);

       
        void WalkTree();
    }
}
