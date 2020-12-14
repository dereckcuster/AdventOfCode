using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            var cypher = inputLines.Select((item) => long.Parse(item)).ToList();
            var cypher2 = Array.ConvertAll(inputLines, (item) => long.Parse(item));
            var cypher3 = Array.ConvertAll(inputLines, long.Parse);

            long invalidValue = GetFristInvalid(cypher);

            Console.WriteLine("Part1: " + invalidValue);

            Console.WriteLine($"Part2: " + GetContiguousBoundsForValue(cypher, invalidValue));

            Console.ReadKey();
        }

        private static long GetFristInvalid(List<long> cypher)
        {
            for (int check = 25; check < cypher.Count; check++)
            {
                var testRange = cypher.GetRange(check - 25, 25);

                bool found = false;
                for (int loopA = 0; loopA < testRange.Count; loopA++)
                {
                    for (int loopB = loopA + 1; loopB < testRange.Count; loopB++)
                    {
                        if (cypher[check] == (testRange[loopA] + testRange[loopB]))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                        break;
                }

                if (!found)
                {
                    var temp = cypher[check];

                    cypher.RemoveRange(check, cypher.Count - check);
                 
                    return temp;
                }
            }

            return -1;            
        }

        private static long GetContiguousBoundsForValue(IList<long> cypher, long value)
        {
            for (int loopA = 0; loopA < cypher.Count; loopA++)
            {
                long lowest = long.MaxValue;
                long highest = 0;
                long cypherValue;

                cypherValue = cypher[loopA];
                if (cypherValue < lowest)
                    lowest = cypherValue;
                if (cypherValue > highest)
                    highest = cypherValue;

                long acc = cypherValue;
                
                for (int loopB = loopA + 1; loopB < cypher.Count; loopB++)
                {
                    cypherValue = cypher[loopB];
                    if (cypherValue < lowest)
                        lowest = cypherValue;
                    if (cypherValue > highest)
                        highest = cypherValue;

                    acc += cypherValue;

                    if (acc == value)
                    {
                        return lowest + highest;
                    }
                    else if (acc > value)
                        break;
                }
            }

            return -1;
        }
    }
}
