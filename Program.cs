var challenge = new Day12();
var day = 12;
var input = File.ReadAllLines($"Day{day}\\input.txt");
var test = File.ReadAllLines($"Day{day}\\test.txt");
Console.WriteLine($"Test output: {challenge.Run(test)}");
Console.WriteLine($"Output: {challenge.Run(input)}");