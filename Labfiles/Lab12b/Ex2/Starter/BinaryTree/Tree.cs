using System;

namespace BinaryTree
{
    public class Tree<TItem> : IBinaryTree<TItem>
        where TItem : IComparable<TItem>
    {
        public TItem NodeData { get; set; }

        public Tree<TItem> LeftTree { get; set; }

        public Tree<TItem> RightTree { get; set; }

        public Tree(TItem nodeValue)
        {
            this.NodeData = nodeValue;
            this.LeftTree = null;
            this.RightTree = null;
        }

        
        public void Add(TItem newItem)
        {
            
            TItem currentNodeValue = this.NodeData;

            
            if (currentNodeValue.CompareTo(newItem) > 0)
            {
                
                if (this.LeftTree == null)
                {
                    this.LeftTree = new Tree<TItem>(newItem);
                }
                else 
                {
                    this.LeftTree.Add(newItem);
                }
            }
            else 
            {
                
                if (this.RightTree == null)
                {
                    this.RightTree = new Tree<TItem>(newItem);
                }
                else 
                {
                    this.RightTree.Add(newItem);
                }
            }
        }

        
        public void WalkTree()
        {
            
            if (this.LeftTree != null)
            {
                this.LeftTree.WalkTree();
            }

            Console.WriteLine(this.NodeData.ToString());

            
            if (this.RightTree != null)
            {
                this.RightTree.WalkTree();
            }
        }

        
        public void Remove(TItem itemToRemove)
        {

            
            if (itemToRemove == null)
            {
                return;
            }

            
            if (this.NodeData.CompareTo(itemToRemove) > 0 && this.LeftTree != null)
            {
                
                if (this.LeftTree.NodeData.CompareTo(itemToRemove) == 0)
                {
                    
                    if (this.LeftTree.LeftTree == null && this.LeftTree.RightTree == null)
                    {
                        this.LeftTree = null;
                    }
                    else 
                    {
                        RemoveNodeWithChildren(this.LeftTree);
                    }
                }
                else
                {
                    
                    this.LeftTree.Remove(itemToRemove);
                }
            }

            
            if (this.NodeData.CompareTo(itemToRemove) < 0 && this.RightTree != null)
            {
               
                if (this.RightTree.NodeData.CompareTo(itemToRemove) == 0)
                {
                    
                    if (this.RightTree.LeftTree == null && this.RightTree.RightTree == null)
                    {
                        this.RightTree = null;
                    }
                    else 
                    {
                        RemoveNodeWithChildren(this.RightTree);
                    }
                }
                else
                {
                    
                    this.RightTree.Remove(itemToRemove);
                }
            }

            
            if (this.NodeData.CompareTo(itemToRemove) == 0)
            {
                               
                if (this.LeftTree == null && this.RightTree == null)
                {
                    return;
                }
                else 
                {
                    RemoveNodeWithChildren(this);
                }
            }
        }



       
        private void RemoveNodeWithChildren(Tree<TItem> node)
        {
            
            if (node.LeftTree == null && node.RightTree == null)
            {
                throw new ArgumentException("Node has no children");
            }

            
            if (node.LeftTree == null ^ node.RightTree == null)
            {
                if (node.LeftTree == null)
                {
                    node.CopyNodeToThis(node.RightTree);
                }
                else
                {
                    node.CopyNodeToThis(node.LeftTree);
                }
            }
            else
            
            {
                
                Tree<TItem> successor = GetLeftMostDescendant(node.RightTree);

                
                node.NodeData = successor.NodeData;

                
                if (node.RightTree.RightTree == null && node.RightTree.LeftTree == null)
                {
                    node.RightTree = null; 
                }
                else
                {
                    node.RightTree.Remove(successor.NodeData);
                }
            }
        }

        
        private void CopyNodeToThis(Tree<TItem> node)
        {
            this.NodeData = node.NodeData;
            this.LeftTree = node.LeftTree;
            this.RightTree = node.RightTree;
        }

        
        private Tree<TItem> GetLeftMostDescendant(Tree<TItem> node)
        {
            while (node.LeftTree != null)
            {
                node = node.LeftTree;
            }
            return node;
        }
    }
}
