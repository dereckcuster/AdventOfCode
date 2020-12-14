using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            var bags = ParseData(inputLines);

            Console.WriteLine("Part1: " + GetNumberOfBagsThatCanContain("shiny gold", bags));
            Console.WriteLine("Part2: " + GetNumberOfContainedBags("shiny gold", bags));

            Console.ReadKey();
        }

        class Bag
        {
            public string name;
            public Dictionary<string, int> contains = new Dictionary<string, int>();
        }

        static List<Bag> ParseData(IList<string> input)
        {
            //drab magenta bags contain 1 vibrant purple bag, 5 dark lime bags, 2 clear silver bags.
            //light gold bags contain no other bags.

            var bags = new List<Bag>();
            
            foreach (var bagData in input)
            {
                var bag = new Bag();

                string parse;

                parse = bagData.Replace("bags", string.Empty).Replace("bag", string.Empty).Replace(".", string.Empty).Replace("contain", "#");

                string[] split = parse.Split("#");

                bag.name = split[0].Trim();

                split = split[1].Split(",");
                foreach (var contain in split)
                {
                    string trimmed = contain.Trim();

                    if (trimmed != "no other")
                        bag.contains.Add(trimmed.Substring(trimmed.IndexOf(" ") + 1), int.Parse(trimmed.Substring(0, trimmed.IndexOf(" "))));                                            
                }

                bags.Add(bag);
            }

            return bags;
        }

        static int GetNumberOfBagsThatCanContain(string bagNameToMatch, IList<Bag> bags)
        {
            var bagNames = new List<string>();

            GetBagThatCanContain(bagNameToMatch, bags, bagNames);

            return bagNames.Count;
        }

        static void GetBagThatCanContain(string bagNameToMatch, IList<Bag> bags, IList<string> bagNames)
        {
            foreach(var bag in bags)
            {
                if (bag.contains.ContainsKey(bagNameToMatch))
                {
                    if (!bagNames.Contains(bag.name))
                        bagNames.Add(bag.name);

                    GetBagThatCanContain(bag.name, bags, bagNames);
                }
            }
        }

        static int GetNumberOfContainedBags(string bagNameToMatch, IList<Bag> bags)
        {
            Bag found = bags.First((test) => test.name == bagNameToMatch);
            
            int count = 0;

            foreach (var content in found.contains)
            {
                count += content.Value;
                count += GetNumberOfContainedBags(content.Key, bags) * content.Value;
            }

            return count;
        }

    }
}
