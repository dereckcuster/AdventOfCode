using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputChars = System.IO.File.ReadAllText("input.txt");

            Console.WriteLine("Part1: " + GetSumOfUniqueYeses(inputChars));
            Console.WriteLine("Part2: " + GetSumOfMatchingYeses(inputChars));

            Console.ReadKey();
        }

        private static int GetSumOfUniqueYeses(string chars)
        {
            // BRUTE FORCE!

            int grandTotal = 0;
            var yeses = new List<char>();
            const char groupSeperator = '|';

            chars = chars.Replace(Environment.NewLine + Environment.NewLine, groupSeperator.ToString());
            chars = chars.Replace(Environment.NewLine, "");

            if (!chars.EndsWith(groupSeperator))
                chars = chars + groupSeperator;

            foreach (var response in chars)
            {
                if (response == groupSeperator)
                {
                    yeses.Clear();
                }
                else if (!yeses.Contains(response))
                {
                    grandTotal++;
                    yeses.Add(response);
                }
            }

            return grandTotal;
        }

        private static int GetSumOfMatchingYeses(string chars)
        {
            // BRUTE FORCE!

            int grandTotal = 0;
            var groupYeses = new List<char>();
            var testYeses = new List<char>();
            const char groupSeperator = '|';
            const char userSeperator = '~';
            bool firstUser = true;

            chars = chars.Replace(Environment.NewLine + Environment.NewLine, userSeperator.ToString() + groupSeperator.ToString());
            chars = chars.Replace(Environment.NewLine, userSeperator.ToString());

            if (!chars.EndsWith(groupSeperator))
                chars = chars + groupSeperator;

            foreach (var response in chars)
            {
                if (response == userSeperator)
                {
                    if (firstUser)
                        firstUser = false;
                    else
                    {
                        groupYeses.RemoveAll((match) => !testYeses.Contains(match));
                        
                        testYeses.Clear();
                    }
                }
                else if (response == groupSeperator)
                {
                    grandTotal += groupYeses.Count();

                    firstUser = true;
                    groupYeses.Clear();
                    testYeses.Clear();
                }
                else if (firstUser)
                {
                    groupYeses.Add(response);
                }                    
                else
                {
                    testYeses.Add(response);
                }
            }

            return grandTotal;
        }
    }
}
