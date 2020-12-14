using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines("input.txt");
            
            List<Passport> passports = ParseData(inputLines);

            List<string> requiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            
            Console.WriteLine("Part1: " + CheckValidPassportFieldExist(requiredFields, passports));

            Console.WriteLine("Part2: " + CheckValidPassportFieldData(requiredFields, ValidateSpecificField, passports));

            Console.ReadKey();
        }

        private class Passport
        {
            public List<PassportField> fields = new List<PassportField>();            
        }

        private class PassportField
        {
            public string id;
            public string data;
        }

        private static List<Passport> ParseData(IList<string> lines)
        {
            //cid:185 byr:1956 eyr:2029 pid:454637740 ecl:hzl hcl:#efcc98 iyr:2019 hgt:73in

            List<Passport> passports = new List<Passport>();

            Passport newPassport = new Passport();

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    passports.Add(newPassport);

                    newPassport = new Passport();
                }
                else
                { 
                    string[] fields;

                    fields = line.Split(' ');

                    foreach (string field in fields)
                    {
                        string[] fieldInfo;
                        fieldInfo = field.Split(':');

                        newPassport.fields.Add(new PassportField() { id = fieldInfo[0], data = fieldInfo[1] });
                    }
                }
            }

            // DON'T FORGET TO ADD THE LAST PASSPORT
            passports.Add(newPassport);

            return passports;
        }

        private static int CheckValidPassportFieldExist(List<string> requiredFields, List<Passport> passports)
        {
            int validCount = 0;

            foreach (var passport in passports)
            {
                validCount++;

                foreach (var requiredField in requiredFields)
                {   
                    if (null == passport.fields.Find((match) => (match.id == requiredField)))
                    {
                        validCount--;
                        break;
                    }   
                }
            }

            return validCount;
        }

        private static int CheckValidPassportFieldData(List<string> requiredFields, Func<PassportField, bool> validateField, List<Passport> passports)
        {
            int validCount = 0;
            
            foreach (var passport in passports)
            {
                validCount++;

                foreach (var requiredField in requiredFields)
                {
                    if (null == passport.fields.Find((match) => ((match.id == requiredField) && validateField(match))))
                    {
                        validCount--;
                        break;
                    }
                }   
            }

            return validCount;
        }

        private static bool ValidateSpecificField(PassportField field)
        {
            try
            {
                switch (field.id)
                {
                    case "byr":
                        int birthYear = int.Parse(field.data);
                        return ((birthYear >= 1920) && (birthYear <= 2002));

                    case "iyr":
                        int issueYear = int.Parse(field.data);
                        return ((issueYear >= 2010) && (issueYear <= 2020));

                    case "eyr":
                        int expireYear = int.Parse(field.data);
                        return ((expireYear >= 2020) && (expireYear <= 2030));

                    case "hgt":
                    
                        int height = int.Parse(String.Concat(field.data.Take(field.data.Length - 2)));
                        string metric = String.Concat(field.data.TakeLast(2));
                        if (metric == "cm")
                            return ((height >= 150) && (height <= 193));
                        else if (metric == "in")
                            return ((height >= 59) && (height <= 76));
                        else
                            return false;

                    case "hcl":
                        return Regex.IsMatch(field.data, "^#[1234567890abcdef]{6}$");

                    case "ecl":
                        return Regex.IsMatch(field.data, "^amb|blu|brn|gry|grn|hzl|oth$");

                    case "pid":
                        return Regex.IsMatch(field.data, @"^\d{9}$");

                    default:
                        return true;
                }
            }
            catch { }

            return false;
        }
    }
}
