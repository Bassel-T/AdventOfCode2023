using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day2
    {

        public static void Part1()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");
                var sum = 0;

                // Capture the number associated with each color
                Regex blue = new Regex(@"([0-9]+) blue");
                Regex red = new Regex(@"([0-9]+) red");
                Regex green = new Regex(@"([0-9]+) green");

                foreach (var line in lines)
                {
                    // Get the game data
                    var plausible = true;
                    var gameData = line.Split(":");
                    var gameId = int.Parse(gameData[0].Substring(5));
                    var rounds = gameData[1].Split(";");

                    foreach (var round in rounds)
                    {
                        // Find each color for the round    
                        var blueMatch = blue.Match(round);
                        var redMatch = red.Match(round);
                        var greenMatch = green.Match(round);

                        // If a color is picked, must be within range
                        plausible &= (!blueMatch.Success || blueMatch.Success && int.Parse(blueMatch.Groups[1].Value) <= 14)
                                    && (!redMatch.Success || redMatch.Success && int.Parse(redMatch.Groups[1].Value) <= 12)
                                    && (!greenMatch.Success || greenMatch.Success && int.Parse(greenMatch.Groups[1].Value) <= 13);
                    }

                    if (plausible)
                    {
                        sum += gameId;
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

                // Set up similar regex
                Regex blue = new Regex(@"([0-9]+) blue");
                Regex red = new Regex(@"([0-9]+) red");
                Regex green = new Regex(@"([0-9]+) green");

                foreach (var line in lines)
                {
                    var maxBlue = 0;
                    var maxRed = 0;
                    var maxGreen = 0;

                    var gameData = line.Split(":");
                    var rounds = gameData[1].Split(";");
                    foreach (var round in rounds)
                    {
                        var blueMatch = blue.Match(round);
                        var redMatch = red.Match(round);
                        var greenMatch = green.Match(round);

                        // Get the maximum of the previous rounds and the current round
                        if (blueMatch.Success)
                            maxBlue = Math.Max(int.Parse(blueMatch.Groups[1].Value), maxBlue);

                        if (redMatch.Success)
                            maxRed = Math.Max(int.Parse(redMatch.Groups[1].Value), maxRed);

                        if (greenMatch.Success)
                            maxGreen = Math.Max(int.Parse(greenMatch.Groups[1].Value), maxGreen);
                    }

                    // Add the "Power" of the set
                    sum += maxBlue * maxGreen * maxRed;
                }

                Console.WriteLine(sum);
            }
        }

    }
}
