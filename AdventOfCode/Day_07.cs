using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day_07 : BaseDay
    {
        private readonly string[] _input;
        private readonly List<Bag> _bags = new();
        private readonly Regex _childBagMatch = new(@"(\d)\s(.+)\s(bags?)?");

        public Day_07()
        {
            _input = File.ReadAllLines(InputFilePath);
            ProcessRules();
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {Solve1()}";
        public override string Solve_2() => @$"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {Solve2()}";


        public long Solve1()
        {
            var potentialBags = new List<Bag>(_bags.Where(x => x.ChildBags.Exists(x => x.Item2.Color == "shiny gold")));
            var moreToProcess = true;
            int currCount = potentialBags.Count;
            while(moreToProcess)
            { 
                foreach(var bag in potentialBags)
                {
                    potentialBags.AddRange(_bags.Where(x => x.ChildBags.Exists(x => x.Item2.Color == bag.Color) && !potentialBags.Contains(x)));
                }
                if(currCount == potentialBags.Count) { moreToProcess = false; }
            }
            return potentialBags.Count;
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
                    var bagDetails = _childBagMatch.Match(child);
                    var childBag = _bags.FirstOrDefault(x => x.Color == bagDetails.Groups[1].Value);
                    if(childBag is null)
                    {
                        childBag = new Bag(bagDetails.Groups[1].Value);
                    }
                    var quantity = int.Parse(bagDetails.Groups[0].Value);
                    bag.ChildBags.Add((quantity, childBag));
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
            ChildBags = new List<(int, Bag)>();
        }
    }
}
