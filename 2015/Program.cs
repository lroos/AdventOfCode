using System.Net.Http.Headers;

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

//Debug(typeof(Day01));

//var days = System.Reflection.Assembly.GetExecutingAssembly()
//    .GetTypes().Where(type => type.IsClass && type.GetInterfaces().Contains(typeof(ISolution)));
//
//foreach (var type in days)
//{    
//    var day = int.Parse(type.Name.Substring(3));
//    Run(day, (ISolution)Activator.CreateInstance(type)!);
//}
var baseAddress = new Uri("https://adventofcode.com");
var cookieContainer = new System.Net.CookieContainer();
cookieContainer.Add(baseAddress, new System.Net.Cookie("session", "53616c7465645f5fbbba60509574d73f7ee14f7559d8316d69ba5adf4ca19e591c82329f77e0db3ae55346c5efd06265", "/"));
var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
var client = new HttpClient(handler) { BaseAddress = baseAddress }; 
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

foreach (var year in Enumerable.Range(2015, 6))
    foreach (var day in Enumerable.Range(1, 25))
    {
        //var input = client.GetStringAsync($"/{year}/day/{day}/input").Result;
        var filename = $"C:\\Dev\\Sandbox\\AdventOfCode\\{year}\\Day{day:D2}\\input.txt";
        var input = File.ReadAllText(filename);
        File.WriteAllText(filename, input.TrimEnd('\n', '\r'));
        //Console.WriteLine($"{year} {day}");
    }

