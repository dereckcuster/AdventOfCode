using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            List<int> allSeatIDs;

            Console.WriteLine("Part1: " + GetHighestSeatID(inputLines, out allSeatIDs));

            Console.WriteLine("Part2: " + GetMissingSeatID(allSeatIDs));

            Console.ReadKey();
        }

        private static int GetHighestSeatID(IList<string> lines, out List<int> allSeatIDs)
        {
            int highestSeatID = 0;

            allSeatIDs = new List<int>();

            foreach (string boardingPass in lines)
            {
                int seatID = Convert.ToInt32(boardingPass.Replace("B", "1").Replace("F", "0").Replace("R", "1").Replace("L", "0"), 2);

                if (seatID > highestSeatID)
                    highestSeatID = seatID;

                allSeatIDs.Add(seatID);
            }

            return highestSeatID;
        }

        private static int GetMissingSeatID(List<int> seatIDs)
        {
            seatIDs.Sort();

            for (int loop = 1; loop < seatIDs.Count; loop++)
            {
                if (seatIDs[loop] != ((seatIDs[loop - 1] + 1)))
                    return seatIDs[loop - 1] + 1;
            }

            return 0;
        }
    }
}