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

        public int Count { get; private set; }

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

            this.Count++;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void DeleteMax()
        {
            throw new NotImplementedException();
        }

        public int GetRank(T element)
        {
            var count = GetRankRecursive(this.Root, element);
            return count;
        }

        public int GetRankRecursive(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            var count = 0;

            if (this.IsGreater(element, current.Value))
            {
                count++;
                count += GetRankRecursive(current.LeftChild, element);
                count += GetRankRecursive(current.RightChild, element);
            }

            //if (this.IsLess(element, current.Value))
            //{
            //    count += GetRankRecursive(current.RightChild, element);
            //}

            return count;
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
                if (this.LeftChild == null)
                {
                    this.LeftChild = toInsert;
                }
            }
            else if (this.IsGreater(element, current.Value))
            {
                current.RightChild = this.InsertRecursive(current.RightChild, element);
                if (this.RightChild == null)
                {
                    this.RightChild = toInsert;
                }
            }

            return current;
        }

        private void Copy(Node<T> root)
        {
            if (root != null)
            {
                this.Insert(root.Value);
                this.Copy(root.LeftChild);
                this.Copy(root.RightChild);
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

        //private int ElementsCount()
        //{
        //    if (this.Root == null)
        //    {
        //        return 0;
        //    }

        //    var count = 0;

        //    this.CountOrderDfs(this.Root, ref count);

        //    return count;
        //}

        //private void CountOrderDfs(Node<T> subtree, ref int count)
        //{
        //    var current = subtree;

        //    if (current == null)
        //    {
        //        return;
        //    }

        //    count++;
        //    CountOrderDfs(current.LeftChild, ref count);
        //    CountOrderDfs(current.RightChild, ref count);
        //}
    }
}
