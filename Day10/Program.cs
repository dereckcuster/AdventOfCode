using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            var adapters = Array.ConvertAll(inputLines, int.Parse).ToList();

            Console.WriteLine("Part1: " + GetJoltageDistribution(adapters));

            Console.WriteLine("Part2: " + GetJoltageArrangements(adapters));

            Console.ReadKey();
        }

        private static int GetJoltageDistribution(List<int> adapters)
        {
            var distribution = new Dictionary<int, int>
            {
                [1] = 0,
                [2] = 0,
                [3] = 0
            };

            adapters.Sort();
            adapters.Add(adapters.Last() + 3);

            int previousJoltage = 0;

            foreach (var joltage in adapters)
            {
                distribution[joltage - previousJoltage]++;

                previousJoltage = joltage;
            }

            return distribution[1] * distribution[3];
        }

        private static long GetJoltageArrangements(List<int> adapters)
        {
            long arrangementsCount = 0;

            adapters.Sort();
            adapters.Add(adapters.Last() + 3);
            adapters.Reverse();

            long multiplier = 1;

            int previousJoltage1 = int.MaxValue;
            int previousJoltage2 = int.MaxValue;
            int previousJoltage3 = int.MaxValue;

            for (int index = 0; index < adapters.Count; index++)
            {
                var joltage = adapters[index];
                               

                previousJoltage3 = previousJoltage2;
                previousJoltage2 = previousJoltage1;
                previousJoltage1 = joltage;
            }

            return arrangementsCount + 1;
        }
    }
}
