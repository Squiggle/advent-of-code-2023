namespace aoc.solutions.tests;

public class SchematicExtractorTests
{
    // this solution will
    // 1. identify the positions of all values (these are always 3 digits)
    [Fact]
    public void NumberIndexes_ReturnIndexesOfValidDigits()
    {
        // arrange
        var row = "301....155..888";

        // act
        var result = new SchematicExtractor(new[] { row }).NumberIndexes().ToList();

        // assert
        Assert.Single(result);
        var firstRow = result.First();
        Assert.Collection(firstRow,
            _ => Assert.Equal(new SchematicExtractor.ValueLineRange(301, 0, 0, 2), _),
            _ => Assert.Equal(new SchematicExtractor.ValueLineRange(155, 0, 7, 9), _),
            _ => Assert.Equal(new SchematicExtractor.ValueLineRange(888, 0, 12, 14), _)
        );
    }

    [Fact]
    public void NumberIndexes_MatchingMask()
    {
        // arrange
        // represents "..123......456." (value in line)
        var valueRanges = new[] {
            new SchematicExtractor.ValueRange(123, 2, 4),
            new SchematicExtractor.ValueRange(456, 11, 13)
        };
        // represents "...###......" (overlapping mask)
        var contactMask = new int[] { 3, 4, 5 };

        // act
        var capturedValuesInLine = SchematicExtractor.CaptureValuesInLine(valueRanges, contactMask);

        // assert
        Assert.Collection(capturedValuesInLine,
            first => Assert.Equal(123, first)
        );
    }

    [Fact]
    public void SchematicExtractor_SumOfMatchingNumbers()
    {
        // arrange
        var lines = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";
        // act
        var result = new SchematicExtractor(lines.Split(Environment.NewLine)).MatchedValues();

        // assert
        Assert.Equal([467, 35, 633, 617, 592, 755, 664, 598], result);
    }
}