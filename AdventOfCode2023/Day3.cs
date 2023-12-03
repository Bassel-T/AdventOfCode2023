using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day3
    {
        public static void Part1()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");

                var sum = 0;
                for (int lineNum = 0; lineNum < lines.Length; lineNum++)
                {
                    for (int index = 0; index < lines[lineNum].Length; index++)
                    {
                        // Skip symbols
                        if (!char.IsNumber(lines[lineNum][index])) continue;

                        var startIndex = index;
                        var scan = 0;

                        while (index < lines[lineNum].Length && char.IsNumber(lines[lineNum][index]))
                        {
                            scan = scan * 10 + (lines[lineNum][index] - '0');
                            index++;
                        }

                        index--;
                                              // Not the first row and contains not number and not dot
                        var symbolSurrounds = (lineNum > 0 && lines[lineNum - 1].Skip(startIndex - 1).Take(index - startIndex + 3).Any(x => !char.IsNumber(x) && x != '.'))
                                              // Value before exists and is not number or dot
                                            || (startIndex > 0 && !char.IsNumber(lines[lineNum][startIndex - 1]) && lines[lineNum][startIndex - 1] != '.')
                                              // Value after exists and si not number or dot
                                            || (index < lines[lineNum].Length - 1 && !char.IsNumber(lines[lineNum][index + 1]) && lines[lineNum][index + 1] != '.')
                                              // Not the last row and contains not number and not dot
                                            || (lineNum < lines.Length - 1 && lines[lineNum + 1].Skip(startIndex - 1).Take(index - startIndex + 3).Any(x => !char.IsNumber(x) && x != '.'));

                        sum += scan;
                    }
                }

                Console.WriteLine(sum);
            }
        }

        public static void Part2()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");

                var sum = 0;
                for (int lineNum = 0; lineNum < lines.Length; lineNum++)
                {
                    for (int index = 0; index < lines[lineNum].Length; index++)
                    {
                        // Skip if not gear
                        if (lines[lineNum][index] != '*') continue;
                        
                        var neighbors = new List<int>();

                        // Find numbers in same row that are neighbors to * position
                        var matches = Regex.Matches(lines[lineNum], @"(\d+)");
                        neighbors.AddRange(matches.Where(x => IsNeighbor(x, lineNum, lineNum, index)).Select(x => int.Parse(x.Value)));

                        if (lineNum > 0)
                        {
                            // Find numbers in previous row that are neighbors to * position
                            matches = Regex.Matches(lines[lineNum - 1], @"(\d+)");
                            neighbors.AddRange(matches.Where(x => IsNeighbor(x, lineNum - 1, lineNum, index)).Select(x => int.Parse(x.Value)));
                        }

                        if (lineNum < lines.Length - 1)
                        {
                            // Find numbers in next row that are neighbors to * position
                            matches = Regex.Matches(lines[lineNum + 1], @"(\d+)");
                            neighbors.AddRange(matches.Where(x => IsNeighbor(x, lineNum + 1, lineNum, index)).Select(x => int.Parse(x.Value)));
                        }

                        // Only take the product and sum if there are exactly two neighbors
                        if (neighbors.Count == 2)
                        {
                            var prod = 1;
                            foreach (var n in neighbors)
                            {
                                prod *= n;
                            }

                            sum += prod;
                        }
                    }
                }

                Console.WriteLine(sum);
            }
        }

        static bool IsNeighbor(Match value, int valueRow, int gearRow, int gearColumn)
        {
            int index = value.Index;
            if (index == -1) { return false; }

            // Vertical distance from gear
            var dy = gearRow - valueRow;

            return Enumerable.Range(index, value.Length).Any(x =>
            {
                // Horizontal distance from any digit in number to gear
                var dx = x - gearColumn;

                /* 
                 * Hacky stuff: Check neighbor by distance. Can't use Manhattan Distance because diagonal
                 * has distance 2, but so does having a space between the number and gear. Use Pythagorean
                 * Theorem to find the proper distance. Diagonal is sqrt(2). Anything farther than that
                 * is not a neighbor (space between would be 2 at least).
                 */ 
                return Math.Sqrt(dx * dx + dy * dy) < 2;
            });
        }
    }
}
