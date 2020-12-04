using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_04 : BaseDay
    {
        private readonly string _input;
        private readonly List<string> _passports;

        private readonly string[] _requiredProps = {"byr","iyr","eyr","hgt","hcl","ecl","pid"};

        public Day_04()
        {
            _input = File.ReadAllText(InputFilePath);
            _passports = _input.Split($"{Environment.NewLine}{Environment.NewLine}").ToList();
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {ValidatePassports()}";

        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {ValidatePassportProperties()}";

        private int ValidatePassports()
        {
            var badPassports = new List<string>();
            foreach(var passport in _passports)
            {
                var fields = new Dictionary<string, string>();
                foreach(var field in passport.Replace(Environment.NewLine, " ").Split(" "))
                {
                    var keyValue = field.Split(':');
                    fields.Add(keyValue[0], keyValue[1]);
                }
                foreach(var prop in _requiredProps)
                {
                    if(!fields.ContainsKey(prop))
                    {
                        badPassports.Add(passport);
                        break;
                    }
                }
            }
            badPassports.ForEach(x => _passports.Remove(x));
            return _passports.Count;
        }

        private int ValidatePassportProperties()
        {
            ValidatePassports();
            var goodPassports = new List<Passport>();
            foreach(var passport in _passports)
            {
                try
                {
                    goodPassports.Add(new Passport(passport));
                }
                catch(InvalidDataException e)
                {
                    //Console.WriteLine(e.Message);
                }
            }
            int testCount = goodPassports.Count(x => x.EyeColor.Length != 3);
            return goodPassports.Count;
        }
    }

    public class Passport
    {
        private readonly Regex yearMatcher = new("^[0-9]{4}$");
        private readonly Regex heightMatcher = new("^[0-9]{2,3}(cm)|(in)$");
        private readonly Regex hairMatcher = new("^#[0-9a-f]{6}$");
        private readonly Regex eyeMatch = new("^(amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth)$");
        private readonly Regex pidMatcher = new("^[0-9]{9}$");


        public Passport(string data)
        {
            foreach(var field in data.Replace(Environment.NewLine," ").Split(" "))
            {
                var keyValue = field.Split(':');
                switch(keyValue[0]){
                    case "byr":
                        BirthYear = int.Parse(keyValue[1]);
                        if(BirthYear < 1920 || BirthYear > 2002 || !yearMatcher.Match(keyValue[1]).Success)
                        {
                            throw new InvalidDataException("BirthYear");
                        }
                    break;
                    case "iyr":
                        IssueYear = int.Parse(keyValue[1]);
                        if(IssueYear < 2010 || IssueYear > 2020 || !yearMatcher.Match(keyValue[1]).Success)
                        {
                            throw new InvalidDataException("IssueYear");
                        }
                    break;
                    case "eyr":
                        ExpirationYear = int.Parse(keyValue[1]);
                        if(ExpirationYear < 2020 || ExpirationYear > 2030 || !yearMatcher.Match(keyValue[1]).Success)
                        {
                            throw new InvalidDataException("ExpirationYear");
                        }
                    break;
                    case "hgt":
                        if (!heightMatcher.Match(keyValue[1]).Success)
                        {
                            throw new InvalidDataException("Height");
                        }
                        if(keyValue[1].EndsWith("cm"))
                        {
                            Height = int.Parse(keyValue[1].Replace("cm",""));
                            if(Height is < 150 or > 193)
                            {
                                throw new InvalidDataException("HeightCM");
                            }
                        }
                        else if(keyValue[1].EndsWith("in"))
                        {
                            Height = int.Parse(keyValue[1].Replace("in",""));
                            if(Height is < 59 or > 76)
                            {
                                throw new InvalidDataException("HeightIN");
                            }
                        }
                        break;
                    case "hcl":
                        if(hairMatcher.Match(keyValue[1]).Success)
                        {
                            HairColor = keyValue[1];
                        }
                        else
                        {
                            throw new InvalidDataException("HairColor");
                        }
                        break;
                    case "ecl":
                        if(eyeMatch.Matches(keyValue[1]).Count == 1)
                        {
                            EyeColor = keyValue[1];
                        }
                        else
                        {
                            throw new InvalidDataException("EyeColor");
                        }
                        break;
                    case "pid":
                        if(pidMatcher.Matches(keyValue[1]).Count == 1)
                        {
                            PassportID = keyValue[1];
                        }
                        else
                        {
                            throw new InvalidDataException("PassportId");
                        }
                        break;
                }
            }
        }
        public int BirthYear {get;}
        public int IssueYear{get;}
        public int ExpirationYear {get;}
        public int Height{get;}
        public string HairColor {get;}
        public string EyeColor {get;}
        public string PassportID {get;}
    }
}