using System.Text.RegularExpressions;

namespace aoc.solutions;

public static partial class CubeGame
{
    [GeneratedRegex("Game (\\d+)")]
    private static partial Regex GameNumberRegex();

    private static readonly Dictionary<string, int> CubeLimits = new()
    {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14
    };

    public static int SumOfValidGameIds(IEnumerable<string> input) =>
        input.Select(input => new { id = GameNumber(input), valid = !CubesExceedMax(MaxFromColourGroups(ParseLineToLookup(input))) })
            .Where(result => result.valid)
            .Sum(result => result.id);

    public static int SumOfPowersFromInput(IEnumerable<string> input) =>
        SumOfPowers(input.Select(line => PowerFromColourGroups(MaxFromColourGroups(ParseLineToLookup(line)))));

    public static bool CubesExceedMax(Dictionary<string, int> maxDrawn)
    {
        return maxDrawn.Any(draw => draw.Value > CubeLimits[draw.Key]);
    }

    public static int GameNumber(string line) => Convert.ToInt32(GameNumberRegex().Match(line).Groups[1].Value);

    public static ILookup<string, int> ParseLineToLookup(string input)
    {
        var resultsOnly = input[(input.IndexOf(':') + 1)..];
        return resultsOnly.Split("; ")
            .Select(RoundToDictionary) // an array of colour/count dictionaries
            .SelectMany(dict => dict)
            .ToLookup(kvp => kvp.Key, kvp => kvp.Value);
    }

    public static Dictionary<string, int> MaxFromColourGroups(ILookup<string, int> colourGroups) =>
                colourGroups.ToDictionary(group => group.Key, group => group.Max());

    private static Dictionary<string, int> RoundToDictionary(string round)
    {
        var draws = round.Split(", ")
            .Select(s => s.Trim()); // "3 blue"
        return ParseDraw(draws)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private static IEnumerable<KeyValuePair<string, int>> ParseDraw(IEnumerable<string> draws)
    {
        foreach (var colourCount in draws)
        {
            var parts = colourCount.Split(" ");
            var count = Convert.ToInt32(parts[0]);
            var colour = parts[1];
            yield return new KeyValuePair<string, int>(colour, count);
        }
    }

    public static int PowerFromColourGroups(Dictionary<string, int> groupMaxCounts) =>
        groupMaxCounts.Values.Aggregate(1, (curr, next) => curr * next);

    public static int SumOfPowers(IEnumerable<int> powers) => powers.Sum();
}