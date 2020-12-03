using AoCHelper;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly string[] _input;

        public Day_02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {_input.Count(x => x.IsValidPart1())}";

        public override string Solve_2() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {_input.Count(x => x.IsValidPart2())}";
    }

    public static class Methods
    {
        public static bool IsValidPart1(this string password)
        {
            var splitPw = password.Split(':');
            var criteria = splitPw[0].Split(' ');
            var counts = criteria[0].Split('-');

            var count = splitPw[1].Count(x => x == criteria[1][0]);

            if(count >= Convert.ToInt32(counts[0]) && count <= Convert.ToInt32(counts[1]))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidPart2(this string password)
        {
            var splitPw = password.Split(": ");
            var criteria = splitPw[0].Split(' ');
            var counts = criteria[0].Split('-');

            var passwordText = splitPw[1];
            var criticalChar = criteria[1][0];

            var i = 0;
            if(passwordText[Convert.ToInt32(counts[0]) - 1] == criticalChar) { i++; }
            if(passwordText[Convert.ToInt32(counts[1]) - 1] == criticalChar) { i++; }

            if(i == 1)
            {
                return true;
            }
            return false;
        }
    }
}
