namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using System.Transactions;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.Root.Count;

        public bool Contains(T element)
        {
            if (this.Root == null)
            {
                return false;
            }
            else
            {
                var current = this.Root;

                while (current != null)
                {
                    if (this.IsLess(element, current.Value))
                    {
                        current = current.LeftChild;
                    }
                    else if (this.IsGreater(element, current.Value))
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            var toInsert = new Node<T>(element, null, null);

            if (this.Root == null)
            {
                this.Root = toInsert;
            }
            else
            {
                this.InsertRecursive(this.Root, element);
            }

        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;

            while (current != null && !this.AreEqual(element, current.Value))
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
            }

            return new BinarySearchTree<T>(current);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrderDfs(this.Root, action);
        }

        public List<T> Range(T lower, T upper)
        {
            if (this.Root == null)
            {
                return null;
            }

            var result = new List<T>();
            var currentSubtree = this.Root;
            this.RangeDfs(lower, upper, result, currentSubtree);
            return result;
        }

        public void DeleteMin()
        {
            this.EnsureNotEmpty();
            var current = this.Root;
            Node<T> previous = null;

            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                while (current.LeftChild != null)
                {
                    current.Count--;
                    previous = current;
                    current = current.LeftChild;
                }

                previous.LeftChild = current.RightChild;
            }
        }

        public void DeleteMax()
        {
            this.EnsureNotEmpty();
            var current = this.Root;
            Node<T> previous = null;

            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else
            {
                while (current.RightChild != null)
                {
                    current.Count--;
                    previous = current;
                    current = current.RightChild;
                }

                previous.RightChild = current.LeftChild;
            }
        }

        public int GetRank(T element)
        {
            return GetRankRecursive(this.Root, element);
        }

        public int GetRankRecursive(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            if (this.IsLess(element, current.Value))
            {
                return this.GetRankRecursive(current.LeftChild, element);
            }
            else if (this.AreEqual(element, current.Value))
            {
                return this.GetNodeCount(current);
            }

            return this.GetNodeCount(current.LeftChild) 
                   + 1 +
                   this.GetRankRecursive(current.RightChild, element);
        }

        private int GetNodeCount(Node<T> current)
        {
            return current == null ? 0 : current.Count;
        }

        private void EnsureNotEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
        }

        private void EachInOrderDfs(Node<T> current, Action<T> action)
        {
            if (current != null)
            {
                this.EachInOrderDfs(current.LeftChild, action);
                action.Invoke(current.Value);
                this.EachInOrderDfs(current.RightChild, action);
            }
        }

        private bool IsLess(T element, T value)
        {
            return element.CompareTo(value) < 0;
        }

        private bool IsGreater(T element, T value)
        {
            return element.CompareTo(value) > 0;
        }

        private bool IsLessOrEqual(T element, T value)
        {
            return element.CompareTo(value) <= 0;
        }

        private bool IsGreaterOrEqual(T element, T value)
        {
            return element.CompareTo(value) >= 0;
        }

        private bool AreEqual(T element, T value)
        {
            return element.CompareTo(value) == 0;
        }

        private Node<T> InsertRecursive(Node<T> current, T element)
        {
            var toInsert = new Node<T>(element, null, null);
            if (current == null)
            {
                return toInsert;
            }

            if (this.IsLess(element, current.Value))
            {
                current.LeftChild = this.InsertRecursive(current.LeftChild, element);
                current.Count++;

                if (this.LeftChild == null)
                {
                    this.LeftChild = toInsert;
                }
            }
            else if (this.IsGreater(element, current.Value))
            {
                current.RightChild = this.InsertRecursive(current.RightChild, element);
                current.Count++;
                if (this.RightChild == null)
                {
                    this.RightChild = toInsert;
                }
            }

            return current;
        }

        private void Copy(Node<T> current)
        {
            if (current != null)
            {
                this.Insert(current.Value);
                this.Copy(current.LeftChild);
                this.Copy(current.RightChild);
            }
        }

        private void RangeDfs(T lower, T upper, List<T> result, Node<T> currentSubtree)
        {
            if (currentSubtree == null)
            {
                return;
            }

            if (this.IsLessOrEqual(lower, currentSubtree.Value) && this.IsGreaterOrEqual(upper, currentSubtree.Value))
            {
                result.Add(currentSubtree.Value);
            }

            if (currentSubtree.LeftChild != null)
            {
                RangeDfs(lower, upper, result, currentSubtree.LeftChild);
            }

            if (currentSubtree.RightChild != null)
            {
                RangeDfs(lower, upper, result, currentSubtree.RightChild);
            }
        }
    }
}
