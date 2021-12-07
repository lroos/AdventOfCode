/// <summary>
/// Zip Ranges
/// </summary>
public class Day5
{    
    public (int, int) Run(string[] input)
    {
        var vents = input
            .Select(x => line.FromString(x))
            .ToArray();

        var straightVents = vents
            .Where(line => line.IsStraight)
            .ToArray();
            
        int overlaps(line[] lines) => lines
            .SelectMany(vent => vent.points)
            .GroupBy(point => point)
            .Where(group => group.Count() >= 2)
            .Count();

        return (overlaps(straightVents), overlaps(vents));
    }
}

record coord
{
    public int X;
    public int Y;
    public coord(int x, int y) { X = x; Y = y; }
    public static coord FromString(string coord)
    {
        var parts = coord.Split(',');
        return new coord(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
    }
};

record line
{
    public coord start;
    public coord end;
    public coord[] points;

    public bool IsStraight => start.X == end.X || start.Y == end.Y;

    static IEnumerable<int> range(int a, int b) => (a == b)
        ? Enumerable.Repeat(a, int.MaxValue)
        : (a < b) ? Enumerable.Range(a, Math.Abs(a - b) + 1) : Enumerable.Range(b, Math.Abs(a - b) + 1).Reverse();

    public static line FromString(string input)
    {
        var parts = input.Split(" -> ");
        var start = coord.FromString(parts[0]);
        var end = coord.FromString(parts[1]);      
        
        var points = range(start.X, end.X)
            .Zip(range(start.Y, end.Y))
            .Select(zip => new coord(zip.First, zip.Second))
            .ToArray();

        return new line()
        {
            start = start,
            end = end,
            points = points
        };
    }
};