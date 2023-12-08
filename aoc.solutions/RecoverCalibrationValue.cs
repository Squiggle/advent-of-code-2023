using System.Text.RegularExpressions;
using System.Linq;

namespace aoc.solutions;

public class RecoverCalibrationValue
{
    public static int FromAmendedLine(string line)
    {
        var numbers = Regex.Replace(line, "[a-z]", "")
            .ToCharArray();
        var firstLast = new string([numbers[0], numbers[^1]]);
        return Convert.ToInt32(firstLast);
    }

    public static int FromAmendedLineDigitified(string line) => FromAmendedLine(line.Digitify());

    public static int FromLines(IEnumerable<string> lines) => lines.Select(FromAmendedLine).Sum();

    public static int FromLinesDigitified(IEnumerable<string> lines) => lines.Select(FromAmendedLineDigitified).Sum();
}
