using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_07 : BaseDay
    {
        private readonly string[] _input;
        private List<Bag> _bags = new List<Bag>();
        private readonly Regex _childBagMatch = new Regex("(\d)\s(.+)\s(bags?)?");

        public Day_07()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            return 0;
        }

        public long Solve2()
        {
            return 0;
        }

        public void ProcessRules()
        {
            foreach(var rule in _input)
            {
                var parentChild = rule.Split(" contain ");
                var parentColor = parentChild[0].Replace(" bags", string.Empty);

                _bags.Add(new Bag(parentColor));
            }
            foreach(var rule in _input)
            {
                var parentChild = rule.Split(" contain ");
                var parentColor = parentChild[0].Replace(" bags", string.Empty);

                var bag = _bags.First(x => x.Color == parentColor);
                var requiredChildren = parentChild[1].Split(", ");
                foreach(var child in requiredChildren)
                {
                    var bagDetails = _childBagMatch.Matches(child);
                    var childBag = _bags.FirstOrDefault(x => x.Color == )
                    bag.ChildBags.Add()
                }
            }
        }
    }

    public class Bag
    {
        public string Color { get; }
        public List<(int, Bag)> ChildBags {get;}

        public Bag(string color)
        {
            Color = color;
            ChildBags = new List<(int, Bag))>();
        }
    }
}
