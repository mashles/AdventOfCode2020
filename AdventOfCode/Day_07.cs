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
        private readonly Dictionary<string, Bag> _bags = new();
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
            var potentialBags = new List<Bag>(_bags.Values.Where(x => x.ChildBags.Exists(x => x.Item2.Color == "shiny gold")));
            var processingBags = new Stack<Bag>(potentialBags);
            while(processingBags.Count > 0)
            {
                foreach (var childBag in _bags.Values.Where(
                    x => x.ChildBags.Exists(
                        x => x.Item2.Color == processingBags.Pop().Color) 
                    && !potentialBags.Contains(x)))
                {
                    processingBags.Push(childBag);
                    potentialBags.Add(childBag);
                }
            }
            return potentialBags.Count;
        }

        public long Solve2()
        {
            int bagCount = 0;
            var processingBags = new Stack<Bag>();
            processingBags.Push(_bags["shiny gold"]);
            while (processingBags.Count > 0)
            {
                foreach (var childBag in processingBags.Pop().ChildBags)
                {
                    bagCount += childBag.Item1;
                    for (int i = 0; i < childBag.Item1; i++)
                    {
                        processingBags.Push(childBag.Item2);
                    }
                }
            }
            return bagCount;
        }

        public void ProcessRules()
        {
            foreach(var rule in _input)
            {
                var parentChild = rule.Split(" contain ");
                var parentColor = parentChild[0].Replace(" bags", string.Empty);
                _bags.Add(parentColor, new Bag(parentColor, parentChild[1]));
            }
            foreach(var bag in _bags)
            {
                ProcessRule(bag.Value);
            }
        }

        public void ProcessRule(Bag bag)
        {
            var requiredChildren = bag.Rule.Split(", ");
            foreach(var child in requiredChildren)
            {
                if (child != "no other bags.")
                {
                    var bagDetails = _childBagMatch.Match(child);
                    bag.ChildBags.Add((int.Parse(bagDetails.Groups[1].Value), _bags[bagDetails.Groups[2].Value]));
                }
            }
        }
    }

    public class Bag
    {
        public string Color { get; }

        public string Rule { get; }

        public List<(int, Bag)> ChildBags { get; }

        public Bag(string color, string rule)
        {
            Color = color;
            ChildBags = new List<(int, Bag)>();
        }

        public override string ToString()
        {
            return Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color);
        }

        public override bool Equals(object obj)
        {
            return Color == (obj as Bag).Color;
        }
    }
}
