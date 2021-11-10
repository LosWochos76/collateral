using AUD.List;
using System;
using System.Collections.Generic;

namespace AUD.Tree
{
	public class BinaryTree
	{
		private TreeNode root = null;
		private int count = 0;

		public int Count
		{
			get
			{
				return count;
			}
		}

		public void Insert(int value)
		{
			if (root == null)
			{
				root = new TreeNode(value);
				count = 1;
			}
			else
			{
				InsertRecursive(root, value);
			}	
		}

		private void InsertRecursive(TreeNode parent, int value)
		{
			if (value <= parent.Value)
			{
				if (parent.Left == null)
				{
					parent.Left = new TreeNode(value);
					count++;
				}
				else
				{
					InsertRecursive(parent.Left, value);
				}
			}
			else
			{
				if (parent.Right == null)
				{
					parent.Right = new TreeNode(value);
					count++;
				}
				else
				{
					InsertRecursive(parent.Right, value);
				}
			}
		}

        private void PrintRecursive(TreeNode node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Value);
                PrintRecursive(node.Left);
                PrintRecursive(node.Right);
            }
        }

        public void Print()
        {
            PrintRecursive(root);
        }

        private void AddToListRecursive(List<int> list, TreeNode node)
        {
            if (node != null)
            {
                list.Add(node.Value);
                AddToListRecursive(list, node.Left);
                AddToListRecursive(list, node.Right);
            }
        }

        public int[] ToArray()
        {
            var list = new List<int>();
            AddToListRecursive(list, root);
            return list.ToArray();
        }

        public override string ToString()
        {
            var array = ToArray();
            var list = new ArrayList(array);
            return list.ToString();
        }

        public bool Contains(int value)
		{
			if (root == null)
				return false;
			else
				return ContainsRecursive(root, value);
		}

		private bool ContainsRecursive(TreeNode node, int value)
		{
			if (node.Value == value)
				return true;
			else if (value < node.Value)
				return node.Left != null && ContainsRecursive(node.Left, value);
			else
				return node.Right != null && ContainsRecursive(node.Right, value);
		}
	}
}