namespace aoc.solutions;

public static class Digitifier
{
    static readonly Dictionary<string, char> DigitMap = new Dictionary<string, char> {
        { "one", '1' },
        { "two", '2' },
        { "three", '3' }, 
        { "four", '4' },
        { "five", '5' },
        { "six", '6' },
        { "seven", '7' },
        { "eight", '8' },
        { "nine", '9' }
    };

    public static string Digitify(this string input) => string.Join(string.Empty, DigitifyEnumerable(input));

    private static IEnumerable<char> DigitifyEnumerable(string input)
    {
        for (var i = 0; i < input.Length; i++) {
            foreach (var (textDigit, numberDigit) in DigitMap) {
                var substring = input.Substring(i);
                if (substring.StartsWith(textDigit) || substring.StartsWith(numberDigit)) {
                    // that's numberwang!
                    yield return numberDigit;
                }
            }
        }
    }
}