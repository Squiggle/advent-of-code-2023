using aoc.solutions;

namespace aoc.solutions.tests;

public class CubeGameTests
{
    private readonly string[] InputFileContent = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green".Split(Environment.NewLine);

    [Fact]
    public void CubeGame_MaxColourFromGroups()
    {
        // parse line from all rounds
        // retaining the highest count of each colour
        // returns dictionary { colour: count }

        // arrange
        var line = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";

        // act
        var result = CubeGame.MaxFromColourGroups(CubeGame.ParseLineToLookup(line));

        // assert
        Assert.Equal(new Dictionary<string, int>
        {
            ["blue"] = 6,
            ["red"] = 4,
            ["green"] = 2
        }, result);
    }

    [Fact]
    public void CubeGame_PowersFromColourGroups()
    {
        // arrange
        var groupMaxCounts = new Dictionary<string, int>
        {
            ["blue"] = 6,
            ["red"] = 4,
            ["green"] = 2
        };

        // act
        var result = CubeGame.PowerFromColourGroups(groupMaxCounts);

        // assert
        Assert.Equal(48, result);
    }

    [Theory]
    [InlineData("Game 1: aaa", 1)]
    [InlineData("Game 48: aaa", 48)]
    [InlineData("Game 501: aaa", 501)]
    public void GameNumberFromLine(string line, int gameId)
    {
        Assert.Equal(gameId, CubeGame.GameNumber(line));
    }

    [Fact]
    public void SumOfValidGameIds()
    {
        // arrange


        // act
        var result = CubeGame.SumOfValidGameIds(InputFileContent);

        // assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void PowerOfCube()
    {
        // arrange
        var powers = new int[] { 48, 12, 1560, 630, 36 };
        //act
        var result = CubeGame.SumOfPowers(powers);
        // assert
        Assert.Equal(2286, result);
    }
}