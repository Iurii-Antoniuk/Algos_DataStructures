using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Immutable_Concurrent
{
    class Program
    {
        public class RemoteBookStock
        {
            public static readonly List<string> Books = new List<string>
            { "Clean code", "C# in depth", "C++ for beginners", "Design patterns in C#", "Kashtanka",
            "The book of five rings"};
        }

        public class StockController
        {
            readonly ConcurrentDictionary<string, int> _stocks = new ConcurrentDictionary<string, int>();

            public void BuyBook (string item, int quantity)
            {
                _stocks.AddOrUpdate(item, quantity, (key, oldValue) => oldValue + quantity);
            }

            public bool TryToRemoveFromStock(string item)
            {
                if (_stocks.TryRemove(item, out int val))
                {
                    Console.WriteLine($"{val} items of {item} were successfully removed from stock");
                    return true;
                }
                return false;
            }

            public bool TrySellBook(string item)
            {
                bool success = false;

                _stocks.AddOrUpdate(item, 
                    itemName => { success = false; return 0; },
                    (itemName, oldValue) =>
                    {
                        if (oldValue == 0)
                        {
                            success = false; 
                            return 0;
                        }
                        else
                        {
                            success = true;
                            return oldValue - 1;
                        }
                    });

                return success;
            }

            public void DisplayStatus()
            {
                foreach (var pair in _stocks)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
        }
        
        private static readonly List<int> largeList = new List<int>(128);

        private static void Generatelist()
        {
            for (int i = 0; i < 100000; i++)
            {
                largeList.Add(i);
            }
        }

        static void BuildImmutableCollectionDemo()
        {
            // Via builder
            var builder = ImmutableList.CreateBuilder<int>();
            foreach (var item in largeList)
            {
                builder.Add(item);
            }
            // Or shorter way
            builder.AddRange(largeList);
            // Convert to immutable
            var immutableList = builder.ToImmutable();

            // Or simpler yet - via LINQ extension
            var immutList = largeList.ToImmutableList();

        }
        
        static void Main(string[] args)
        {
            //Task t1 = Task.Run(() => );
            //Task t2 = Task.Run(() => );

            Task.WaitAll();

            //ListDemo();
            //QueueDemo();
            //StackDemo();
            Console.Read();
        }

        public static void ListDemo()
        {
            var list = ImmutableList<int>.Empty;
            list = list.Add(2);
            list = list.Add(3);
            list = list.Add(4);
            list = list.Add(5);
            list = list.Add(6);

            PrintCollection(list);

            Console.WriteLine("Remove 4, then at index 2");
            list = list.Remove(4);
            list = list.RemoveAt(2);

            PrintCollection(list);

            Console.WriteLine("Insert 1 at 0, 4 at 3");
            list = list.Insert(0, 1);
            list = list.Insert(3, 4);

            PrintCollection(list);
        }
        public static void QueueDemo()
        {
            var queue = ImmutableQueue<int>.Empty;

            queue = queue.Enqueue(1);
            queue = queue.Enqueue(2);

            PrintCollection<int>(queue);

            int first = queue.Peek();
            Console.WriteLine($"First item: {first}");

            queue = queue.Dequeue(out first);
            Console.WriteLine($"Last before Dequeue: {first}, after Dequeue: {queue.Peek()}");
        }
        
        public static void StackDemo()
        {
            var stack = ImmutableStack<int>.Empty;
            stack = stack.Push(1);
            stack = stack.Push(2);

            int last = stack.Peek();
            Console.WriteLine($"Last item: {last}");

            stack = stack.Pop(out last);
            Console.WriteLine($"Last before Pop: {last}, after Pop: {stack.Peek()}");
        }

        private static void PrintCollection<T>(IEnumerable<T> collection)
        {
            foreach(var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}
