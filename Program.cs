var challenge = new Day9();
var day = 9;
var input = File.ReadAllLines($"Day{day}\\input.txt");
var test = File.ReadAllLines($"Day{day}\\test.txt");
Console.WriteLine($"Test output: {challenge.Run(test)} should be ");
Console.WriteLine($"Output: {challenge.Run(input)}");