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

            var processingBags = new Stack<Bag>(potentialBags);
            while(processingBags.Count > 0)
            {
                var bag = processingBags.Pop();
                foreach (var childBag in _bags.Where(x => x.ChildBags.Exists(x => x.Item2.Color == bag.Color) && !potentialBags.Contains(x)))
                {
                    processingBags.Push(childBag);
                    potentialBags.Add(childBag);
                }
            }
            return potentialBags.Count;
        }

        public long Solve2()
        {
            var goldBag = _bags.First(x => x.Color == "shiny gold");
            int bagCount = 0;
            var processingBags = new Stack<Bag>();
            processingBags.Push(goldBag);
            while (processingBags.Count > 0)
            {
                var bag = processingBags.Pop();
                foreach (var childBag in bag.ChildBags)
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
                    if (child != "no other bags.")
                    {
                        var bagDetails = _childBagMatch.Match(child);
                        var color = bagDetails.Groups[2].Value;
                        var childBag = _bags.FirstOrDefault(x => x.Color == color);
                        var quantity = int.Parse(bagDetails.Groups[1].Value);
                        bag.ChildBags.Add((quantity, childBag));
                    }
                }
            }
        }
    }

    public class Bag
    {
        public string Color { get; }

        public List<(int, Bag)> ChildBags { get; }

        public Bag(string color)
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
