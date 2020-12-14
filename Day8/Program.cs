using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            var codings = ParseData(inputLines);

            int acc;
            GetAccValueAfterFirstRecursion(codings, out acc);
            Console.WriteLine("Part1: " + acc);

            Console.WriteLine("Part2: " + GetAccValueAfterFixAndComplete(codings));

            Console.ReadKey();
        }

        static List<KeyValuePair<string, int>> ParseData(IList<string> input)
        {
            var codings = new List<KeyValuePair<string, int>>();

            foreach (var coding in input)
            {
                string[] split = coding.Split(" ");

                codings.Add(new KeyValuePair<string, int>(split[0], int.Parse(split[1])));
            }

            return codings;
        }

        private static bool GetAccValueAfterFirstRecursion(List<KeyValuePair<string, int>> codings, out int acc)
        {
            acc = 0;
            var visited = new List<int>();
            int currentLine = 0;

            while (true)
            {
                if (visited.Contains(currentLine))
                    return false;

                if (currentLine == codings.Count)
                    return true;

                visited.Add(currentLine);

                switch (codings[currentLine].Key)
                {
                    case "acc":
                        acc += codings[currentLine].Value;
                        currentLine++;
                        break;

                    case "jmp":
                        currentLine += codings[currentLine].Value;
                        break;

                    case "nop":
                        currentLine++;
                        break;
                }
            }
        }

        private static int GetAccValueAfterFixAndComplete(List<KeyValuePair<string, int>> codings)
        {
            int acc = 0;

            for (int testLine = 0; testLine < codings.Count; testLine++)
            {
                ExchangeCommand(codings, testLine);

                if (GetAccValueAfterFirstRecursion(codings, out acc))
                    return acc;

                ExchangeCommand(codings, testLine);
            }

            // NO FIX!
            return -1;
        }

        private static void ExchangeCommand(List<KeyValuePair<string, int>> codings, int line)
        {
            switch (codings[line].Key)
            {
                case "jmp":
                    codings.Insert(line, new KeyValuePair<string, int>("nop", codings[line].Value));
                    codings.RemoveAt(line + 1);
                    break;

                case "nop":
                    codings.Insert(line, new KeyValuePair<string, int>("jmp", codings[line].Value));
                    codings.RemoveAt(line + 1);
                    break;
            }
        }
    }
}
