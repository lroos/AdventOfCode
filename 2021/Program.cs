void Test(int day, ISolution solution)
{
    var test = File.ReadAllLines($"Day{day}\\test.txt");
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

var days = System.Reflection.Assembly.GetExecutingAssembly()
    .GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(ISolution)));

//Debug(typeof(Day25));

//foreach (var type in days)
//{    
//    var day = int.Parse(type.Name.Substring(3));
//    Run(day, (ISolution)Activator.CreateInstance(type)!);
//}


//foreach (var year in Enumerable.Range(2015, 6))
    foreach (var day in Enumerable.Range(1, 25))
    {
        var className = $"Day{day:D2}";
        var path = className;
        Directory.CreateDirectory(path);
        File.WriteAllText($"{path}\\{className}.cs", @"public class {0} : ISolution
{
    (long, long) Run(string[] input)
    {
        return (0, 0);
    }
}".Replace("{0}", className), System.Text.Encoding.UTF8);
        File.CreateText($"{path}\\README.md");
        File.CreateText($"{path}\\input.txt");
        File.CreateText($"{path}\\test.txt");
    }