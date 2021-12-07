string[] Load(int day) => File.ReadAllLines($"Day{day}\\input.txt");
var challenge = new Day7();
var input = Load(7);
Console.WriteLine(challenge.Run(input));