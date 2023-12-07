using System.IO;
using aoc.solutions;

var input = args[0];
// day 1.1
Console.WriteLine(RecoverCalibrationValue.FromLines(File.ReadAllLines(input)));