using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_10 : BaseDay
    {
        private readonly List<int> _input;
        public Day_10()
        {
            _input = ReadAllLinesAsIntList(InputFilePath);
            _input.Add(0);
            _input.Add(_input.Max() + 3);
            _input.Sort();
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            var counter = new Dictionary<int, int>()
            {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 }
            };
            for(int i = 0; i < _input.Count-1; i++)
            {
                counter[_input[i+1]-_input[i]]++;
            }
            return counter[3] * counter[1];
        }

        public long Solve2()
        {
            List<long> permutations = new List<long>() { 1 };
            for(int i = 1; i < _input.Count; i++)
            {
                var minIndex = Math.Max(0, i - 3);
                int countWithinRange = _input.GetRange(minIndex, i-minIndex).Count(x => x >= _input[i] - 3);

                permutations.Add(permutations.TakeLast(countWithinRange).Sum());
            }
            return permutations.Last();
        }

        public List<int> ReadAllLinesAsIntList(string path)
        {
            List<int> intList = new List<int>();
            foreach(var line in File.ReadAllLines(path))
            {
                intList.Add(Convert.ToInt32(line));
            }
            return intList;
        }
    }
}
