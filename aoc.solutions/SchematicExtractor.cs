using System.Text.RegularExpressions;

namespace aoc.solutions;

public class SchematicExtractor(IEnumerable<string> schemaLines)
{
    private readonly IEnumerable<string> schemaLines = schemaLines;

    private static readonly List<char> numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    internal IEnumerable<ValueLineRange[]> NumberIndexes() => schemaLines
            .Select((line, index) =>
                ValueRangesFromLine(line.ToCharArray())
                    .Select(_ => new ValueLineRange(_.Value, index, _.StartPosition, _.EndPosition))
                    .ToArray());

    public IEnumerable<int> MatchedValues()
    {
        var lineMasks = schemaLines.SelectMany(MaskPositionsForLine).ToArray();
        return schemaLines
        // urgh
            .SelectMany((value, index) =>
            CaptureValuesInLine(
                ValueRangesFromLine(value.ToCharArray()),
                lineMasks
                    .Where(_ => _.Row == index)
                    .Select(lm => lm.Column).ToArray()));
    }

    public int Sum() => MatchedValues().Sum();

    private static IEnumerable<int> IndexesOfMask(string line)
    {
        for (var i = 0; i < line.Length; i++)
        {
            if (line[i] != '.' && !numbers.Contains(line[i]))
            {
                yield return i - 1;
                yield return i;
                yield return i + 1;
            }
        }
    }

    private static IEnumerable<MaskPosition> MaskPositionsForLine(string line, int row)
    {
        foreach (var lineMask in IndexesOfMask(line))
        {
            yield return new MaskPosition(lineMask, row - 1);
            yield return new MaskPosition(lineMask, row);
            yield return new MaskPosition(lineMask, row + 1);
        }
    }

    private static IEnumerable<ValueRange> ValueRangesFromLine(char[] line)
    {
        var curr = new List<char>();
        var enumerator = line.AsEnumerable().GetEnumerator();
        var index = 0;
        while (enumerator.MoveNext())
        {
            if (numbers.Contains(enumerator.Current))
            {
                curr.Add(enumerator.Current);
            }
            else if (curr.Count != 0)
            {
                var value = Convert.ToInt32(new string(curr.ToArray()));
                yield return new ValueRange(value, index - curr.Count, index - 1);
                curr.Clear();
            }
            index++;
        }

        if (curr.Count != 0)
        {
            var value = Convert.ToInt32(new string(curr.ToArray()));
            yield return new ValueRange(value, index - curr.Count, index - 1);
        }
    }

    internal static IEnumerable<int> CaptureValuesInLine(IEnumerable<ValueRange> valueRanges, int[] contactMask) =>
        valueRanges
            .Where(vlr => Enumerable
                .Range(vlr.StartPosition, vlr.EndPosition - vlr.StartPosition)
                .Intersect(contactMask)
                .Any())
            .Select(vlr => vlr.Value);

    internal record ValueLineRange(int Value, int Row, int StartPosition, int EndPosition);
    internal record ValueRange(int Value, int StartPosition, int EndPosition);

    internal record MaskPosition(int Row, int Column);
}