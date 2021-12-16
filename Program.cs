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
    Console.WriteLine($"{solution.GetType().Name} Output: {solution.Run(input)} solved in {s.ElapsedMilliseconds}ms");
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

Debug(typeof(Day15));