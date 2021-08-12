using System;

namespace ConsoleApp1
{
    public enum Colors
    {
        White = 1,
        Black = 2,
        Pink = 4,
        Yellow = 8,
        Red = 16,
        Green = 32,
        Blue = 64,
        Violet = 128
    }

    class Program
    {
        public delegate string SayHello(string name);
        static void Main(string[] args)
        {
            /// BITWISE STUFF
            //byte a = 25;
            //byte b = 7;

            //byte res = (byte)~a;

            //Console.WriteLine($"{Convert.ToString(a, 2).PadLeft(8, '0')} ~");
            ////Console.WriteLine($"{Convert.ToString(b, 2).PadLeft(8, '0')}");
            //Console.WriteLine("-----------------");
            //Console.WriteLine($"{Convert.ToString(res, 2).PadLeft(8, '0')}");

            var colorSelection = (byte)(Colors.Black | Colors.Green | Colors.Violet);
            Console.WriteLine($"{Convert.ToString(colorSelection, 2).PadLeft(8, '0')}");
            /// BITWISE STUFF

            /// CLOSURES in C#
            SayHello myFunc = var1 => $"Hello {var1}";
            Console.WriteLine(myFunc("John"));

            var inc = GetAFuncLocal();
            Console.WriteLine(inc(5));
            Console.WriteLine(inc(6));
            Console.WriteLine(inc(6));

            Console.ReadKey();
        }

        //******3 WAYS TO IMPLEMENT A CLOSURE in C# *******

        // a) using an internal LAMBDA expression
        public static Func<int, int> GetAFuncLambda()
        {
            var myVar = 1;
            Func<int, int> inc = var1 =>
            {
                myVar = myVar + 1;
                return var1 + myVar;
            };
            return inc;
        }

        // b) using an internal anonymous method (via "delegate" keyword) 
        public static Func<int, int> GetAFuncAnonymous()
        {
            var myVar = 1;
            Func<int, int> inc = delegate (int var1)
            {
                myVar = myVar + 1;
                return var1 + myVar;
            };
            return inc;
        }

        // c) By defining and returning a LOCAL FUNCTION (the best one)
        public static Func<int, int> GetAFuncLocal()
        {
            var myVar = 1;
            int inc(int var1)
            {
                myVar = myVar + 1;
                return var1 + myVar;
            }
            return inc;
        }
    }
}
