using System.Text.RegularExpressions;

namespace aoc.solutions;

public static class CubeGame
{
    private static readonly Dictionary<string, int> CubeLimits = new()
    {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14
    };

    public static int SumOfValidGameIds(IEnumerable<string> input) =>
        input.Select(input => new { id = GameNumber(input), valid = !CubesExceedMax(ParseLine(input)) })
            .Where(result => result.valid)
            .Sum(result => result.id);

    public static bool CubesExceedMax(Dictionary<string, int> maxDrawn)
    {
        return maxDrawn.Any(draw => draw.Value > CubeLimits[draw.Key]);
    }

    public static int GameNumber(string line)
    {
        return Convert.ToInt32(Regex.Match(line, "Game (\\d+)").Groups[1].Value);
    }

    public static Dictionary<string, int> ParseLine(string input)
    {
        var resultsOnly = input[(input.IndexOf(":") + 1)..];
        return resultsOnly.Split("; ")
            .Select(RoundToDictionary) // an array of colour/count dictionaries
            .SelectMany(dict => dict)
            .ToLookup(kvp => kvp.Key, kvp => kvp.Value)
            .ToDictionary(group => group.Key, group => group.Max());
    }

    private static Dictionary<string, int> RoundToDictionary(string round)
    {
        var draws = round.Split(", ")
            .Select(s => s.Trim()); // "3 blue"
        return ParseDraw(draws)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private static IEnumerable<KeyValuePair<string, int>> ParseDraw(IEnumerable<string> draws)
    {
        foreach (var colourCount in draws) {
            var parts = colourCount.Split(" ");
            var count = Convert.ToInt32(parts[0]);
            var colour = parts[1];
            yield return new KeyValuePair<string, int>(colour, count);
        }
    }
}