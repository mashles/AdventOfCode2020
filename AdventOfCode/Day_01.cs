using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly List<int> _input;

        public Day_01()
        {
            _input = ReadAllLinesAsIntList(InputFilePath);
            _input.Sort();
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";

        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1(){
            long result = 0;
            _input.ForEach(
                x =>  { _input.ForEach(y => { if(y + x == 2020) { result = x*y; } } ); }
            );
            return result;
        }

        public long Solve2(){
            long result = 0;
            _input.ForEach(
                x =>  { _input.ForEach(
                    y => { _input.ForEach(z => { if(z + y + x == 2020) { result = z*x*y; } } ); }
                ); }
            );
            return result;
        }

        public static List<int> ReadAllLinesAsIntList(string path)
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
