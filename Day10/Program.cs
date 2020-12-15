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
            adapters.Add(0);
            adapters.Sort();
            adapters.Add(adapters.Last() + 3);

            return GetJoltageArrangementsFromIndex(adapters);
        }

        static Dictionary<int, long> cache = new Dictionary<int, long>();

        private static long GetJoltageArrangementsFromIndex(List<int> adapters, int startIndex = 0)
        {
            if (cache.ContainsKey(startIndex))
                return cache[startIndex];

            if (startIndex == (adapters.Count - 1))
                return 1;

            long arrangementsCount = GetJoltageArrangementsFromIndex(adapters, startIndex + 1);

            if (startIndex < (adapters.Count - 2))
                if ((adapters[startIndex + 2] - adapters[startIndex]) <= 3)
                    arrangementsCount += GetJoltageArrangementsFromIndex(adapters, startIndex + 2);

            if (startIndex < (adapters.Count - 3))
                if ((adapters[startIndex + 3] - adapters[startIndex]) <= 3)
                    arrangementsCount += GetJoltageArrangementsFromIndex(adapters, startIndex + 3);

            cache[startIndex] = arrangementsCount;

            return arrangementsCount;            
        }
    }
}
