using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_11 : BaseDay
    {
        private List<string> _input;
        private readonly int _lineLength;
        public Day_11()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
            _lineLength = _input[0].Length;
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            var applyRulesCount = 0;
            while(ApplyRules() > 0)
            {
                applyRulesCount++;
            }
            return applyRulesCount;
        }

        public long Solve2()
        {
            return 0;
        }


        public int ApplyRules()
        {
            var changeCount = 0;
            var newMap = new List<string>();
            for(int i = 0; i < _input.Count; i++)
            {
                var newRow = new char[_lineLength];
                for(int c = 0; c < newRow.Length; c++)
                {
                    newRow[c] = GetNewSeatState(c, i);
                    if(newRow[c] != _input[i][c])
                    {
                        changeCount++;
                    }
                }
                newMap.Add(newRow.ToString());
            }
            _input = newMap;
            return changeCount;
        }

        public char GetNewSeatState(int x, int y)
        {
            var currentState = _input[y][x];
            switch(currentState)
            {
                case 'L':
                    if(GetAdjacentCount(x, y) == 0)
                        return '#';
                    break;
                case '#':
                    if(GetAdjacentCount(x, y) >= 4)
                        return 'L';
                    break;
            }
            return currentState;
        }

        public int GetAdjacentCount(int x, int y)
        {
            int count = 0;
            var yRange = (Math.Max(0, y-1), Math.Min(_input.Count-1, y+1));
            var xRange = (Math.Max(0, x-1), Math.Min(_lineLength-1, x+1));

            for(int iy = yRange.Item1; iy < yRange.Item2; iy++)
            {
                for(int ix = xRange.Item1; ix < xRange.Item2; ix++)
                {
                    if(iy != y && ix != x && _input[iy][ix] == '#')
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
