using System.Text.RegularExpressions;
using System.Linq;

namespace aoc.solutions;

public class RecoverCalibrationValue
{
    public static int FromAmendedLine(string amendedLine)
    {
        var digitified = amendedLine.Digitify();
        var numbers = Regex.Replace(digitified, "[a-z]", "")
            .ToCharArray();
        var firstLast = new string([numbers[0], numbers[^1]]);
        return Convert.ToInt32(firstLast);
    }

    public static int FromLines(IEnumerable<string> lines) => lines.Select(FromAmendedLine).Sum();
}
