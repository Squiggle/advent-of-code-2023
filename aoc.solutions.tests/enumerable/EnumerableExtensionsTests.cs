namespace aoc.solutions.extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void RollingCollection()
    {
        var collection = Enumerable.Range(1, 5);

        var result = collection.Rolling(3);

        Assert.Collection(result,
            _ => Assert.Equal([1], _),
            _ => Assert.Equal([1, 2], _),
            _ => Assert.Equal([1, 2, 3], _),
            _ => Assert.Equal([2, 3, 4], _),
            _ => Assert.Equal([3, 4, 5], _)
        );
    }
}