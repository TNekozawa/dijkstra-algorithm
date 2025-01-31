using System;
using System.Collections.Generic;

namespace Models.Graph.Algorithms
{
    public class SortedQueue<T> where T : IComparable<T>
    {
        private readonly List<T> sortedQueue;
        public SortedQueue()
        {
            sortedQueue = [];
        }
        public void Add(T item)
        {
            sortedQueue.Add(item);
        }
        public void Sort()
        {
            sortedQueue.Sort();
        }
        public void Clear()
        {
            sortedQueue.Clear();
        }
        public int Count()
        {
            return sortedQueue.Count;
        }
        public bool Contains(T item)
        {
            return sortedQueue.Contains(item);
        }
        public T Pop()
        {
            T item = sortedQueue[0];
            sortedQueue.RemoveAt(0);
            return item;
        }
    }
}
