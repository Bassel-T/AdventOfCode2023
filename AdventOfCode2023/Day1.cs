using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day1
    {
        public static void Part1()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");

                // Find numbers
                Regex regex = new Regex(@"[0-9]");
                var sum = lines.Sum(line =>
                {
                    var matches = regex.Matches(line);

                    // sum += 10x + y; x => first digit, y => second digit
                    if (matches.Any())
                        return int.Parse(matches.First().Value) * 10 + int.Parse(matches.Last().Value);

                    return 0;
                });

                Console.WriteLine(sum);
            }
        }

        public static void Part2()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");
                Regex regex = new Regex(@"[0-9]");
                var sum = lines.Sum(line =>
                {
                    // Numbers can overlap, keep first and last digits of each, convert rest to number
                    line = line.Replace("one", "o1e")
                               .Replace("two", "t2o")
                               .Replace("three", "t3e")
                               .Replace("four", "f4r")
                               .Replace("five", "f5e")
                               .Replace("six", "s6x")
                               .Replace("seven", "s7n")
                               .Replace("eight", "e8t")
                               .Replace("nine", "n9e");

                    var matches = regex.Matches(line);

                    // sum += 10x + y; x => first digit, y => second digit
                    if (matches.Any())
                        return int.Parse(matches.First().Value) * 10 + int.Parse(matches.Last().Value);

                    return 0;
                });

                Console.WriteLine(sum);
            }
        }
    }
}
