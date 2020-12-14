using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            int[] linesInt = Array.ConvertAll(inputLines, int.Parse);

            TwoSum2020(linesInt);
            ThreeSum2020(linesInt);
            ThreeSum2020Ex(linesInt);

            Console.ReadKey();
        }

        static void TwoSum2020(int[] linesInt)
        {       
            // NOT SURE IF THIS OPTIMISTAION IS OPTIMAL!
            Array.Sort(linesInt);

            for (int loopA = 0; loopA < linesInt.Length; loopA++)
            {
                for (int loopB = loopA+1; loopB < linesInt.Length; loopB++)
                {
                    int value1 = linesInt[loopA];
                    int value2 = linesInt[loopB];

                    if (2020 == (value1 + value2))
                        Console.WriteLine($"{value1} x {value2} = " + (value1*value2));
                    else if ((value1 + value2) > 2020)
                        break;
                }
            }
        }

        static void ThreeSum2020(int[] linesInt)
        {
            // NOT SURE IF THIS OPTIMISTAION IS OPTIMAL!
            Array.Sort(linesInt);

            for (int loopA = 0; loopA < linesInt.Length; loopA++)
            {
                for (int loopB = loopA + 1; loopB < linesInt.Length; loopB++)
                {
                    for (int loopC = loopB + 1; loopC < linesInt.Length; loopC++)
                    {
                        int value1 = linesInt[loopA];
                        int value2 = linesInt[loopB];
                        int value3 = linesInt[loopC];

                        if (2020 == (value1 + value2 + value3))                             
                            Console.WriteLine($"{value1} x {value2} x {value3} = " + (value1 * value2 * value3));
                        else if ((value1 + value2 + value3) > 2020)
                            break;
                    }
                }
            }
        }

        static void ThreeSum2020Ex(int[] linesInt)
        {
            // NOT SURE IF THIS OPTIMISTAION IS OPTIMAL!
            Array.Sort(linesInt);

            SumRecurse(linesInt, 2020, 0, 0, 3);
        }

        static List<int> _values = new List<int>();

        static void SumRecurse(int[] linesInt, int test, int currentSum, int start, int depth)
        {
            for (int loop = start; loop < linesInt.Length; loop++)
            {
                int value = linesInt[loop];
                _values.Add(value);

                int newSum = (currentSum + value);

                try
                {
                    if (1 == depth)
                    {
                        if (test == newSum)
                        {
                            _values.ForEach((value) => Console.Write(value + " "));
                            Console.WriteLine(_values.Aggregate((x, y) => (x * y)));
                        }
                        else if (newSum > test)
                            break;
                    }
                    else
                        SumRecurse(linesInt, test, newSum, loop + 1, depth - 1);
                }
                finally
                {
                    _values.RemoveAt(_values.Count - 1);
                }
            }
        }
    }
}
