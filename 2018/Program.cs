void Test(int day, ISolution solution)
{
    var test = File.ReadAllLines($"Day{day:D2}\\test.txt");
    Console.WriteLine($"Test output: {solution.Run(test)}");
}

void Run(int day, ISolution solution)
{
    var s = new System.Diagnostics.Stopwatch();    
    var input = File.ReadAllLines($"Day{day:D2}\\input.txt");
    s.Start();
    Console.Write($"{solution.GetType().Name,-6} Output: {solution.Run(input),-23} solved in {s.ElapsedMilliseconds,4}ms");
    var source = File.ReadAllLines($"..\\..\\..\\Day{day:D2}\\Day{day}.cs");
    Console.WriteLine($" with {source.Count(l => !string.IsNullOrWhiteSpace(l)),-3} lines of code.");
}

void Debug(Type type)
{ 
    var day = int.Parse(type.Name.Substring(3));
    var solution = (ISolution)Activator.CreateInstance(type)!;
    Test(day, solution);
    Run(day, solution);
}

Debug(typeof(Day01));

//var days = System.Reflection.Assembly.GetExecutingAssembly()
//    .GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(ISolution)));
//
//foreach (var type in days)
//{    
//    var day = int.Parse(type.Name.Substring(3));
//    Run(day, (ISolution)Activator.CreateInstance(type)!);
//}