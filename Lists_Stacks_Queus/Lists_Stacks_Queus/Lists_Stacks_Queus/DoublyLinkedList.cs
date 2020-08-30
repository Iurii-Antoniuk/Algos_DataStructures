using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lists_Stacks_Queus
{
    public class DoublyLinkedList<T>
    {
        public DoublyLinkedNode<T> Head { get; private set; }
        public DoublyLinkedNode<T> Tail { get; private set; }

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public void AddFirst(T value)
        {
            AddFirst(new DoublyLinkedNode<T>(value));
        }

        public void AddFirst(DoublyLinkedNode<T> doublyLinkedNode)
        {
            DoublyLinkedNode<T> temp = Head;

            Head = doublyLinkedNode;
            Head.Next = temp;

            if (IsEmpty)
            {
                Tail = Head;
            }
            else
            {
                temp.Previous = Head;
            }
            Count++;
        }

        public void AddLast(T value)
        {
            AddLast(new DoublyLinkedNode<T>(value));
        }

        public void AddLast(DoublyLinkedNode<T> doublyLinkedNode)
        {
            if (IsEmpty)
                Head = doublyLinkedNode;
            else
            {
                Tail.Next = doublyLinkedNode;
                doublyLinkedNode.Previous = Tail;
            }
            Tail = doublyLinkedNode;
            Count++;
        }

        public void RemoveFirst()
        {
            if (IsEmpty)
                throw new IOException();

            Head = Head.Next;
            Count--;

            if (IsEmpty)
                Tail = null;
            else
                Head.Previous = null;
        }

        public void RemoveLast()
        {
            if (IsEmpty)
                throw new IOException();

            Tail = Tail.Previous;
            Count--;

            if (IsEmpty)
                Head = null;
            else
                Tail.Next = null;
        }
    }
}
