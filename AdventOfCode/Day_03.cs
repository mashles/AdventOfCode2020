using AoCHelper;
using System.IO;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private readonly string[] _input;

        public Day_03()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {GetTreesEncountered(3, 1)}";

        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2:
        {
            GetTreesEncountered(1, 1)*
            GetTreesEncountered(3, 1)*
            GetTreesEncountered(5, 1)*
            GetTreesEncountered(7, 1)*
            GetTreesEncountered(1, 2)
        }";

        public long GetTreesEncountered(int right, int down)
        {
            var treesEncountered = 0;
            var i = right;
            for(int p = down; p < _input.Length; p+=down)
            {
                if(_input[p][i] == '#')
                {
                    treesEncountered++;
                }
                i+=right;
                if(i >= _input[p].Length)
                {
                    i = i % _input[p].Length;
                }
            }
            return treesEncountered;
        }
    }
}
