namespace aoc.solutions.extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> Rolling<T>(this IEnumerable<T> source, int rollingCount)
    {
        var rollingCollection = new List<T>();
        foreach (var item in source)
        {
            var start = rollingCollection.Count >= rollingCount ? 1 : 0;
            rollingCollection = rollingCollection[start..];
            rollingCollection.Add(item);
            yield return rollingCollection;
        }
    }
}