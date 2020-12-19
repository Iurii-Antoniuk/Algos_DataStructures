using System;
using System.Collections.Generic;
using System.Text;

namespace Heaps
{
    public class MaxHeap<T> where T : IComparable<T>
    {
        private const int DefaultCapacity = 4;
        private T[] _heap;

        public int Count { get; private set; }
        public bool IsFull => Count == _heap.Length;
        public bool IsEmpty => Count == 0;

        public MaxHeap():this(DefaultCapacity)
        {

        }

        public MaxHeap(int capacity)
        {
            _heap = new T[capacity];
        }

        public void Insert(T value)
        {
            if (IsFull)
            {
                var newHeap = new T[_heap.Length * 2];
                Array.Copy(_heap, 0, newHeap, 0, _heap.Length);
                _heap = newHeap;
            }
            _heap[Count] = value;
            Swim(Count);
            Count++;
        }

        public IEnumerable<T> Values()
        {
            for(int i = 0; i < Count; i++)
            {
                yield return _heap[i];
            }
        }
        private void Swim(int indexOfSwimmingItem)
        {
            T newValue = _heap[indexOfSwimmingItem];

            while (indexOfSwimmingItem > 0 && ParentLess(indexOfSwimmingItem))
            {
                _heap[indexOfSwimmingItem] = GetParent(indexOfSwimmingItem);
                indexOfSwimmingItem = ParentIndex(indexOfSwimmingItem);
            }

            _heap[indexOfSwimmingItem] = newValue;

            bool ParentLess(int swimmingItemIndex)
            {
                return newValue.CompareTo(GetParent(swimmingItemIndex)) > 0;
            }
        }

        private T GetParent(int index)
        {
            return _heap[ParentIndex(index)];
        }

        private int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }
    }
}
