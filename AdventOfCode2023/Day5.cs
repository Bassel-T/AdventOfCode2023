using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day5
    {
        public static void Part1() {
            using (var reader = new StreamReader("input.txt"))
            {
                var mappings = reader.ReadToEnd().Split("\r\n\r\n").Select(x => x.Split("\r\n").ToList()).ToList();
                List<long> seeds = Regex.Matches(mappings[0][0], @"(\d)+").Select(x => long.Parse(x.Value)).ToList();
                
                foreach (var mapping in mappings.Skip(1))
                {
                    var shifts = mapping.Skip(1).Select(x => x.Split(" ").Select(x => long.Parse(x)).ToList()).ToList();

                    seeds = seeds.Select(seed =>
                    {
                        var properShift = shifts.Where(shift => seed >= shift[1]).MinBy(shift => seed - shift[1]);

                        if (properShift == null) { return seed; }

                        var offset = seed - properShift[1];

                        if (offset >= properShift[2]) { return seed; }

                        return properShift[0] + offset;
                    }).ToList();
                }

                Console.WriteLine($"Min Location: {seeds.Min()}");
            }
        }

        public static void Part2() {
            using (var reader = new StreamReader("input.txt"))
            {
                var mappings = reader.ReadToEnd().Split("\r\n\r\n").Select(x => x.Split("\r\n").ToList()).ToList();
                var readNumbers = Regex.Matches(mappings[0][0], @"(\d)+");

                var minLocation = long.MaxValue;

                for (int i = 0; i < readNumbers.Count / 2; i++)
                {
                    var baseSeed = long.Parse(readNumbers[2 * i].Value);
                    for (long offset = 0, c = long.Parse(readNumbers[2 * i + 1].Value); offset < c; offset++)
                    {
                        var newSeed = baseSeed + offset;

                        foreach (var mapping in mappings.Skip(1))
                        {
                            var shifts = mapping.Skip(1).Select(x => x.Split(" ").Select(x => long.Parse(x)).ToList()).ToList();
                            var properShift = shifts.Where(shift => newSeed >= shift[1]).MinBy(shift => newSeed - shift[1]);
                            
                            if (properShift == null) { continue; }

                            var shiftBy = newSeed - properShift[1];
                            
                            if (shiftBy >= properShift[2]) { continue; }
                            
                            newSeed = properShift[0] + shiftBy;
                        }

                        if (newSeed <= minLocation) { minLocation = newSeed; }
                    }
                }

                Console.WriteLine($"Min Location: {minLocation}");
            }
        }
    }
}
