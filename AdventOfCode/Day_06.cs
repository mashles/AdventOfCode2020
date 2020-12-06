using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        private readonly string _input;
        public Day_06()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            return SplitByGroup().Sum(x => x.Replace(Environment.NewLine, string.Empty).Distinct().Count());
        }

        public long Solve2()
        {
            return SplitByGroup().Sum(x => x.Split(Environment.NewLine)
                    .Select(y => y.ToCharArray())
                    .Aggregate<IEnumerable<char>>((prev, next) => prev.Intersect(next)).Count());
        }

        public IEnumerable<string> SplitByGroup()
        {
            return _input.Split($"{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
