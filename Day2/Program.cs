using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");

            List<PasswordPolicy> passwords = ParseData(inputLines);

            Console.WriteLine("Part1: " + CheckPasswordsCharCount(passwords));
            Console.WriteLine("Part2: " + CheckPasswordsCharPosition(passwords));

            Console.ReadKey();
        }

        private class PasswordPolicy
        {
            public int minCount; //USED FOR POSITION1 IN PART 2!
            public int maxCount; //USED FOR POSITION2 IN PART 2!
            public char charToMatch;
            public string passwordToCheck;
        }

        private static List<PasswordPolicy> ParseData(IList<string> lines)
        {
            // 1-8 n: dpwpmhknmnlglhjtrbpx
            
            List<PasswordPolicy> passwords = new List<PasswordPolicy>();

            foreach (string line in lines)
            {
                PasswordPolicy newPassword = new PasswordPolicy();
                string[] split;
                
                split = line.Split('-');
                newPassword.minCount = int.Parse(split[0]);
                split = split[1].Split(' ');
                newPassword.maxCount = int.Parse(split[0]);
                newPassword.charToMatch = split[1][0];
                newPassword.passwordToCheck = split[2].TrimStart();

                passwords.Add(newPassword);
            }

            return passwords;
        }

        private static int CheckPasswordsCharCount(List<PasswordPolicy> passwords)
        {
            int validCount = 0;

            foreach (PasswordPolicy password in passwords)
            {
                int numberOfMatchingChars = password.passwordToCheck.Count(element => element == password.charToMatch);

                if ((numberOfMatchingChars >= password.minCount) && (numberOfMatchingChars <= password.maxCount))
                    validCount++;
            }

            return validCount;
        }



        private static int CheckPasswordsCharPosition(List<PasswordPolicy> passwords)
        {
            int validCount = 0;

            foreach (PasswordPolicy password in passwords)
            {
                if ((password.passwordToCheck[password.minCount - 1] == password.charToMatch) ^ (password.passwordToCheck[password.maxCount - 1] == password.charToMatch))
                    validCount++;
            }

            return validCount;
        }
    }
}
