using aoc.solutions;
using Xunit;

namespace aoc.solutions.tests;

public class DigitifierTests {

    [Theory]
    [InlineData("onexxx", "1")]
    [InlineData("onexxxtwo", "12")]
    [InlineData("twonexxx", "21")]
    [InlineData("one2onexxx", "121")]
    [InlineData("1twoneightxx", "1218")]
    public void ExtractSymbolsInString(string input, string expect) {
        Assert.Equal(expect, input.Digitify());
    }
}