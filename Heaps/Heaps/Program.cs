using System;

namespace Heaps
{
    class Program
    {
        static void Main(string[] args)
        {
            var HeapTest = new MaxHeap<int>();

            HeapTest.Insert(24);
            HeapTest.Insert(37);
            HeapTest.Insert(17);
            HeapTest.Insert(28);
            HeapTest.Insert(31);
            HeapTest.Insert(29);
            HeapTest.Insert(15);
            HeapTest.Insert(12);
            HeapTest.Insert(20);

            Console.WriteLine(HeapTest.Peek());
            Console.WriteLine(HeapTest.Remove());
            Console.WriteLine(HeapTest.Peek());

            HeapTest.Insert(40);
            Console.WriteLine(HeapTest.Peek());

            foreach (var val in HeapTest.Values())
            {
                Console.Write($"{val} ");
            }

            Console.ReadKey();
        }
    }
}
