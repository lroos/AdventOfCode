/// <summary>
/// 
/// </summary>
public class Day7
{
    public (int, int) Run(string[] input)
    {
        var positions = input.First().Split(",").Select(x => int.Parse(x));
        int min = positions.Min(), max = positions.Max();
        var diffs = Enumerable.Range(min, max)
            .Select(x => positions.Select(p => Math.Abs(x - p)));

        var minFuel = diffs.Min(d => d.Sum());
        var crabFuel = (int)diffs.Min(d => d.Sum(x => x * new[] { 1, x }.Average()));

        return (minFuel, crabFuel);
    }
}