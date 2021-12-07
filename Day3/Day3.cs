/// <summary>
/// Golf day
/// </summary>
public class Day3
{       
    public (int, int) Run(string[] input)
    {
        var numbers = input.Select(i => Convert.ToInt32(i, 2));
        var range = Enumerable.Range(0, 12);
        int count(IEnumerable<int> values, int check) => values.Count(n => (n & check) == check);

        var gamma = range
            .Select(i => new { pos = 1 << i, count = count(numbers, 1 << i)})
            .Where(x => x.count > input.Length / 2)
            .Aggregate(0, (acc, x) => x.pos | acc);

        var epsilon = gamma ^ 4095;

        int reduce(bool top) => range.Reverse().Select(i => 1 << i)
            .Aggregate(numbers, (acc, check) => acc.Count() <= 1 ? acc 
                : acc.Where(n => (n & check) == (((count(acc, check) >= acc.Count() / 2m) ^ top) ? check : 0))
                     .ToArray())
            .First();

        var oxy = reduce(true);
        var co2 = reduce(false);

        return (gamma * epsilon, oxy * co2);
    }
}