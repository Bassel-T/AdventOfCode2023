using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day4
    {
        public static void Part1()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");
                var total = 0;

                foreach (var line in lines)
                {
                    // Card [id] : [numbers] | [winning]
                    var values = line.Split(new char[] {':', '|'});

                    // Get individual numbers
                    var numbers = values[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var winnings = values[2].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    // Get matches. Round score = 2 ^ (matches - 1); 1, 2, 4, 8, 16, etc.
                    var matches = numbers.Count(x => winnings.Contains(x));

                    if (matches > 0)
                        total += Convert.ToInt32(Math.Pow(2, matches - 1));
                }

                Console.WriteLine(total);
            }
        }

        public static void Part2()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                var lines = reader.ReadToEnd().Split("\r\n");
                Dictionary<int, int> cardsOfRound = new Dictionary<int, int>();

                // We start with one of every card
                for (int i = 0; i < lines.Length; i++)
                {
                    cardsOfRound[i + 1] = 1;
                }

                foreach (var line in lines)
                {
                    var values = line.Split(new char[] { ':', '|' });
                    
                    // Same logic as before, but also track current card number
                    var cardId = int.Parse(values[0].Substring(5));
                    var numbers = values[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var winnings = values[2].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    // For n matches, add copy of next n cards
                    // Handle copies of current one at the same time
                    var matches = numbers.Count(x => winnings.Contains(x));

                    for (int i = cardId + 1; i < cardId + matches + 1; i++)
                    {
                        if (cardsOfRound.ContainsKey(i))
                        {
                            cardsOfRound[i] += cardsOfRound[cardId];
                        }
                        else
                        {
                            cardsOfRound[i] = cardsOfRound[cardId];
                        }
                    }
                }

                // Sum of all values is the number of cards in play
                Console.WriteLine(cardsOfRound.Sum(x => x.Value));
            }
        }
    }
}
