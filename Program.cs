using System.Collections.Generic;
using static System.Console;

namespace ConvertWordsToNumber
{
    class Program
    {
        private static readonly IDictionary<string, long> TOKENS = GetTokens();
        private static readonly IList<string> MULTIPLIERS = GetMultipliers();

        static void Main(string[] args)
        {
            Write("Please enter a number in words (ex. one million five hundred thousand eighty five): ");
            var input = ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }
            else
            {
                var number = ParseWordsAndConvertToNumber(input);
                WriteLine($"Result: {number.ToString("N0")}");
            }
        }

        private static IDictionary<string, long> GetTokens()
        {
            IDictionary<string, long> tokens = new Dictionary<string, long>();

            tokens.Add("ninety", 90L);
            tokens.Add("eighty", 80L);
            tokens.Add("seventy", 70L);
            tokens.Add("sixty", 60L);
            tokens.Add("fifty", 50L);
            tokens.Add("forty", 40L);
            tokens.Add("thirty", 30L);
            tokens.Add("twenty", 20L);
            tokens.Add("nineteen", 19L);
            tokens.Add("eighteen", 18L);
            tokens.Add("seventeen", 17L);
            tokens.Add("sixteen", 16L);
            tokens.Add("fifteen", 15L);
            tokens.Add("fourteen", 14L);
            tokens.Add("thirteen", 13L);
            tokens.Add("twelve", 12L);
            tokens.Add("eleven", 11L);
            tokens.Add("ten", 10L);
            tokens.Add("nine", 9L);
            tokens.Add("eight", 8L);
            tokens.Add("seven", 7L);
            tokens.Add("six", 6L);
            tokens.Add("five", 5L);
            tokens.Add("four", 4L);
            tokens.Add("three", 3L);
            tokens.Add("two", 2L);
            tokens.Add("one", 1L);

            tokens.Add("hundred", 100L);
            tokens.Add("thousand", 1000L);
            tokens.Add("million", 1000000L);

            return tokens;
        }

        private static IList<string> GetMultipliers()
        {
            IList<string> multipliers = new List<string>();

            multipliers.Add("hundred");
            multipliers.Add("thousand");
            multipliers.Add("million");

            return multipliers;
        }

        private static long ParseWordsAndConvertToNumber(string words)
        {
            words = words.ToLower().Trim().Replace(" ", string.Empty);

            return FindMatchingTokenAndGetValue(0L, words);
        }

        private static long FindMatchingTokenAndGetValue(long previousValue, string remainingWords)
        {
            if (remainingWords.Length == 0)
            {
                return previousValue;
            }

            foreach (var token in TOKENS)
            {
                if (IsTokenMatch(remainingWords, token.Key))
                {
                    if (!IsMultiplier(token.Key))
                    {
                        if (previousValue % 1000 == 0 || previousValue % 100 == 0 && token.Value >= 100)
                        {
                            return FindMatchingTokenAndGetValue(token.Value, remainingWords.Substring(token.Key.Length)) + previousValue;
                        }
                        else
                        {
                            return FindMatchingTokenAndGetValue(previousValue + token.Value, remainingWords.Substring(token.Key.Length));
                        }
                    }
                    else // token is a multiplier
                    {
                        return FindMatchingTokenAndGetValue(previousValue * token.Value, remainingWords.Substring(token.Key.Length));
                    }
                }
            }

            return previousValue;
        }

        private static bool IsMultiplier(string token)
        {
            return MULTIPLIERS.Contains(token);
        }

        private static bool IsTokenMatch(string words, string token)
        {
            return words.IndexOf(token) == 0;
        }
    }
}
