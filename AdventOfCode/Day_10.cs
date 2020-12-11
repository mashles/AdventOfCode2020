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
        private long _permCounter = 0;
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
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 }
            };
            for(int i = 0; i < _input.Count-1; i++)
            {
                counter[_input[i+1]-_input[i]]++;
            }
            return counter[3] * counter[1];
        }

        public long Solve2()
        {
            return RunToEnd(0);
        }

        private int RunToEnd(int index)
        {
            var result = 0;
            if(index == _input.Count - 1)
                result++;
            if(index+3 < _input.Count && _input[index+3] - _input[index] <= 3)
            {
                result += RunToEnd(index+3);
            } 
            if(index+2 < _input.Count && _input[index+2] - _input[index] <= 3)
            {
                result += RunToEnd(index+2);
            }
            if(index+1 < _input.Count && _input[index+1] - _input[index] <= 3)
            {
                result += RunToEnd(index+1);
            }
            return result;
        }

        private void SetDifVal(int dif, Dictionary<int, int> counter)
        {
            if(dif <= 3)
            {
                counter[dif]++;
            }
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
