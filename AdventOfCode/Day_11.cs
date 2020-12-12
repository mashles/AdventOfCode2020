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
            while (ApplyRules() > 0) ;
            return _input.Sum(x => x.Count(y => y == '#'));
        }

        public long Solve2()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
            while (ApplyRules2() > 0) ;
            return _input.Sum(x => x.Count(y => y == '#'));
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
                newMap.Add(new string(newRow));
            }
            _input = newMap;
            return changeCount;
        }

        public int ApplyRules2()
        {
            var changeCount = 0;
            var newMap = new List<string>();
            for (int i = 0; i < _input.Count; i++)
            {
                var newRow = new char[_lineLength];
                for (int c = 0; c < newRow.Length; c++)
                {
                    newRow[c] = GetNewSeatState2(c, i);
                    if (newRow[c] != _input[i][c])
                    {
                        changeCount++;
                    }
                }
                newMap.Add(new string(newRow));
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

        public char GetNewSeatState2(int x, int y)
        {
            var currentState = _input[y][x];
            switch (currentState)
            {
                case 'L':
                    if (GetVisibleCount(x, y) == 0)
                        return '#';
                    break;
                case '#':
                    if (GetVisibleCount(x, y) >= 5)
                        return 'L';
                    break;
            }
            return currentState;
        }

        public int GetVisibleCount(int x, int y)
        {
            int count = 0;
            

            for(int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        var currX = x;
                        var currY = y;
                        bool hit = false;
                        char hitChar = '.';
                        while (!hit && currX < _lineLength && currX >= 0 && currY < _input.Count && currY >= 0)
                        {
                            if (!(currY == y && currX == x) && _input[currY][currX] != '.')
                            {
                                hit = true;
                                hitChar = _input[currY][currX];
                            }
                            currX = currX += j;
                            currY = currY += i;
                        }
                        if (hit == true && hitChar == '#')
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public int GetAdjacentCount(int x, int y)
        {
            int count = 0;
            var yRange = (Math.Max(0, y-1), Math.Min(_input.Count-1, y+1));
            var xRange = (Math.Max(0, x-1), Math.Min(_lineLength-1, x+1));

            for(int iy = yRange.Item1; iy <= yRange.Item2; iy++)
            {
                for(int ix = xRange.Item1; ix <= xRange.Item2; ix++)
                {
                    if(iy == y && ix == x) { continue; }
                    if(_input[iy][ix] == '#')
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
