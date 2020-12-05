using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_05 : BaseDay
    {
        private readonly List<string> _input;
        public Day_05()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            return GetAllSeatIds().Max();
        }

        public long Solve2()
        {
            var allIds = GetAllSeatIds();
            return allIds.Single(x => allIds.Contains(x) && !allIds.Contains(x+1) && allIds.Contains(x+2)) + 1;
        }

        public List<int> GetAllSeatIds()
        {
            var allIds = new List<int>();
            foreach(var input in _input)
            {
                allIds.Add(GetRowOrColumn(input.Substring(0, 7))*8+GetRowOrColumn(input.Substring(7, 3)));
            }
            return allIds;
        }

        public int GetRowOrColumn(string letters)
        {
            string binary = letters.Replace("F","0").Replace("B","1").Replace("L","0").Replace("R","1");
            int number = Convert.ToInt32(binary, 2);
            return number;
        }
    }
}
