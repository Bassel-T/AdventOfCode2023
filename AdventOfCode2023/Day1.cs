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
                Regex regex = new Regex(@"[0-9]");
                var sum = 0;

                foreach(var line in lines)
                {
                    var matches = regex.Matches(line);

                    if (matches.Any())
                        sum += int.Parse(matches.First().Value) * 10 + int.Parse(matches.Last().Value);
                }

                Console.WriteLine(sum);
            }
        }

        public static void Part2()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");
                Regex regex = new Regex(@"[0-9]");
                var sum = 0;

                foreach (var line in lines)
                {
                    var newline = line.Replace("one", "o1e");
                    newline = newline.Replace("two", "t2o");
                    newline = newline.Replace("three", "t3e");
                    newline = newline.Replace("four", "f4r");
                    newline = newline.Replace("five", "f5e");
                    newline = newline.Replace("six", "s6x");
                    newline = newline.Replace("seven", "s7n");
                    newline = newline.Replace("eight", "e8t");
                    newline = newline.Replace("nine", "n9e");
                    var matches = regex.Matches(newline);

                    if (matches.Any())
                    {
                        var num = int.Parse(matches.First().Value) * 10 + int.Parse(matches.Last().Value);
                        Console.WriteLine($"{line}: {num}");
                        sum += num;
                    }
                }

                Console.WriteLine(sum);
            }
        }
    }
}
