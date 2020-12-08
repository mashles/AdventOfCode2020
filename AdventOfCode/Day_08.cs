using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_08 : BaseDay
    {
        private readonly List<string> _input;
        public Day_08()
        {
            _input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            int i = 0;
            var accumulator = 0;
            var runCount = new Dictionary<int, int>();
            while(true)
            {
                if(runCount.ContainsKey(i))
                {
                    return accumulator;
                }
                else
                {
                    runCount.Add(i, 1);
                }
                switch(_input[i].Substring(0, 3))
                {
                    case "acc":
                        if(_input[i][4] == '+')
                            accumulator += int.Parse(_input[i].Substring(5, _input[i].Length-5));
                        else
                            accumulator -= int.Parse(_input[i].Substring(5, _input[i].Length-5));
                        i++;
                        break;
                    case "jmp":
                        if(_input[i][4] == '+')
                            i += int.Parse(_input[i].Substring(5, _input[i].Length-5));
                        else
                            i -= int.Parse(_input[i].Substring(5, _input[i].Length-5));
                        break;
                    case "nop":
                        i++;
                        break;    
                }
            }
        }

        public long Solve2()
        {
            for (int j = 0; j < _input.Count; j++)
            {
                int i = 0;
                var accumulator = 0;
                if (!_input[j].StartsWith("jmp") && !_input[j].StartsWith("nop"))
                {
                    continue;
                }
                var runCount = new Dictionary<int, bool>();
                var switchOccured = false;
                while (i < _input.Count)
                {
                    if (runCount.ContainsKey(i))
                        break;
                    else
                        runCount.Add(i, true);
                    switch (_input[i].Substring(0, 3))
                    {
                        case "acc":
                            if (_input[i][4] == '+')
                                accumulator += int.Parse(_input[i].Substring(5, _input[i].Length - 5));
                            else
                                accumulator -= int.Parse(_input[i].Substring(5, _input[i].Length - 5));
                            i++;
                            break;
                        case "jmp":
                            if (i == j && !switchOccured)
                            {
                                switchOccured = true;
                                goto case "nop";
                            }
                            if (_input[i][4] == '+')
                                i += int.Parse(_input[i].Substring(5, _input[i].Length - 5));
                            else
                                i -= int.Parse(_input[i].Substring(5, _input[i].Length - 5));
                            break;
                        case "nop":
                            if (i == j && !switchOccured)
                            {
                                switchOccured = true;
                                goto case "jmp";
                            }
                            i++;
                            break;
                    }
                    if(i >= _input.Count)
                    {
                        return accumulator;
                    }
                }
            }
            return 0;
        }
    }
}
