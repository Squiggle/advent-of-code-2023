using System.Diagnostics;
using aoc.solutions;

var days = new Dictionary<string, Func<IEnumerable<string>, int>[]>
{
    ["01.1.txt"] = [
        RecoverCalibrationValue.FromLines,
        RecoverCalibrationValue.FromLinesDigitified
    ],
    ["02.1.txt"] = [
        CubeGame.SumOfValidGameIds,
        CubeGame.SumOfPowersFromInput
    ]
};

// converts input path to an IEnumerable list of lines from that file
Func<string, Func<IEnumerable<string>, int>, int> LoadAndExecute =
    (inputFilePath, execute) => execute(File.ReadLines(Path.Combine("..", "inputs", inputFilePath)));

// exec in sequence
var stopwatch = Stopwatch.StartNew();
foreach (var (dayInputFileName, parts) in days)
{
    Console.WriteLine($"Day {dayInputFileName[..2]}");
    foreach (var part in parts)
    {
        stopwatch.Restart();
        Console.WriteLine($"The result is {LoadAndExecute(dayInputFileName, part)}");
        stopwatch.Stop();
        Console.WriteLine($"  calculated in {stopwatch.ElapsedMilliseconds}ms{Environment.NewLine}");
    }
    Console.WriteLine();
}