using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_09 : BaseDay
    {
        private readonly long[] _input;
        private readonly Dictionary<int, List<long>> _sums;
        private readonly Dictionary<int, Dictionary<long, int>> _contiguousSums;
        private long _part1Answer;

        public Day_09()
        {
            _input = ReadAllLinesAsLongList(InputFilePath);
            _sums = PreloadSums();
            _contiguousSums = PreloadContiguousSums();
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            for(int i = 25; i < _input.Length; i++)
            {
                if(!_sums.Where(x => x.Key >= (i-25) && x.Key < i).SelectMany(x => x.Value).Contains(_input[i]))
                {
                    _part1Answer = _input[i];
                    return _input[i];
                }
            }
            return 0;
        }

        public long Solve2()
        {
            var indexes = GetFirstAndLastIndex();
            Console.WriteLine(indexes.Item1 + " - " + indexes.Item2);
            var range = _input[indexes.Item1..indexes.Item2];
            return range.Min()+range.Max();
        }

        public long[] ReadAllLinesAsLongList(string path)
        {
            var lines = File.ReadAllLines(path);
            long[] intList = new long[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                intList[i] = long.Parse(lines[i]);
            }
            return intList;
        }

        public Dictionary<int, List<long>> PreloadSums()
        {
            var sums = new Dictionary<int, List<long>>();
            for(int i = 0; i < _input.Length; i++)
            {
                sums.Add(i, GetAllSumsExcludingSame(_input[i]));
            }
            return sums;
        }

        public List<long> GetAllSumsExcludingSame(long number)
        {
            var sumList = new List<long>();
            foreach(var otherNumber in _input)
            {
                if(otherNumber != number)
                {
                    sumList.Add(number + otherNumber);
                }
            }
            return sumList;
        }

        public Dictionary<int, Dictionary<long, int>> PreloadContiguousSums()
        {
            var sums = new Dictionary<int, Dictionary<long, int>>();
            for(int i = 0; i < _input.Length; i++)
            {
                sums.Add(i, GetAllSumsContiguous(_input[i], i));
            }
            return sums;
        }

        public Dictionary<long, int> GetAllSumsContiguous(long number, int index)
        {
            var sumList = new Dictionary<long, int>();
            long contiguousSum = number;
            for(int i = index + 1; i < _input.Length; i++)
            {
                contiguousSum += _input[i];
                sumList.Add(contiguousSum, i);
            }
            return sumList;
        }

        public (int, int) GetFirstAndLastIndex()
        {
            var first = 0;
            foreach(var list in _contiguousSums)
            {
                if(list.Value.TryGetValue(_part1Answer, out int last))
                {
                    first = list.Key;
                    return(first, last);
                }
            }
            return (0, 0);
        }
    }
}
