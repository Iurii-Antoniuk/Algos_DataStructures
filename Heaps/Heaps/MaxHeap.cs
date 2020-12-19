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

        public IEnumerable<T> Values()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _heap[i];
            }
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

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The fucking heap contains absolutely no elements");

            return _heap[0];
        }

        public T Remove()
        {
            return Remove(0);
        }

        private T Remove(int index)
        {
            if (IsEmpty)
                throw new InvalidOperationException("The fucking heap contains absolutely no elements");

            T removedValue = _heap[index];
            _heap[index] = _heap[Count - 1];
            Count--;

            Sink(index);

            return removedValue;
        }

        private void Sink(int indexOfSinkingItem)
        {
            int heapMaxIndex = Count - 1;

            while (indexOfSinkingItem < heapMaxIndex)
            {
                int indexOfLeftChild = 2 * indexOfSinkingItem + 1;
                int indexOfRightChild = 2 * indexOfSinkingItem + 2;

                if (indexOfLeftChild > heapMaxIndex)
                    break;
                else
                {
                    int indexOfChildToSwap = GetIndexOfChildToSwap(indexOfLeftChild, indexOfRightChild);
                    if (_heap[indexOfSinkingItem].CompareTo(_heap[indexOfChildToSwap]) < 0)
                    {
                        Swap(indexOfSinkingItem, indexOfChildToSwap);
                        indexOfSinkingItem = indexOfChildToSwap;
                    }
                    else
                        break;
                }
            }
        }

        private void Swap(int indexOfSinkingItem, int indexOfChildToSwap)
        {
            T tmp = _heap[indexOfSinkingItem];
            _heap[indexOfSinkingItem] = _heap[indexOfChildToSwap];
            _heap[indexOfChildToSwap] = tmp;
        }

        private int GetIndexOfChildToSwap(int indexOfLeftChild, int indexOfRightChild)
        {
            if (indexOfRightChild > (Count - 1))
                return indexOfLeftChild;
            else
            {
                int indexToReturn = (_heap[indexOfLeftChild].CompareTo(_heap[indexOfRightChild]) > 0) 
                    ? indexOfLeftChild : indexOfRightChild;
                return indexToReturn;
            }
        }
    }
}
