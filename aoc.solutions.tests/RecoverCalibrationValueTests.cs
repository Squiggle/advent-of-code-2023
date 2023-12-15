using aoc.solutions;

namespace aoc.solutions.tests;

public class RecoverCalibrationValueTests
{
    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    public void CalibrationValue_FromAmendedLine_GivesTwoDigitValue(string line, int expectedResult)
    {
        var result = RecoverCalibrationValue.FromAmendedLine(line);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("onetwothreeight", 18)]
    public void CalibrationValue_FromAmendedLineDigitified_GivesTwoDigitValue(string line, int expectedResult)
    {
        var result = RecoverCalibrationValue.FromAmendedLineDigitified(line);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void CalibrationValue_FromLines_GivesSum()
    {
        // arrange
        var lines = new[] {
            "a1gg2sf",
            "4lksjfda8"
        };
        // act
        var result = RecoverCalibrationValue.FromLines(lines);
        // assert
        Assert.Equal(60, result);
    }
}