using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            Console.WriteLine("Part1: " + CheckTreesHit(3, 1, inputLines));

            var slopes = new List<(int xStep, int yStep)> { (1,1), (3,1), (5,1), (7,1), (1,2) };

            Console.WriteLine("Part2: " + CheckTreesHitMultipleSlopes(slopes, inputLines));

            Console.WriteLine("Part2Ex: " + slopes.Select(slope => CheckTreesHit(slope.xStep, slope.yStep, inputLines)).Aggregate(1, (long total, long next) => total * next));

            Console.ReadKey();
        }

        private static long CheckTreesHit(int xStep, int yStep, string[] lines)
        {
            // .....#.##......#..##..........#

            int xPos = 0;
            int hitCount = 0;

            for (int yPos = 0; yPos < lines.Length; yPos += yStep)
            {
                string line = lines[yPos];

                if (line[xPos % line.Length] == '#')
                    hitCount++;

                xPos += xStep;
            }

            return hitCount;
        }

        private static long CheckTreesHitMultipleSlopes(List<(int xStep, int yStep)> slopes, string[] lines)
        {
            long product = 1;

            foreach (var slope in slopes)
            {
                product *= CheckTreesHit(slope.xStep, slope.yStep, lines);
            }

            return product;
        }
    }
}