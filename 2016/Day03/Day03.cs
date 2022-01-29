public class Day03 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var triangles = input.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray())
            .ToArray();

        int valid(int[][] triangles) => triangles
            .Select(c => c.OrderBy(x => x).ToArray())
            .Count(t => t.Length == 3 && t[2] < t[1] + t[0]);

        var triangles2 = triangles.Select(t => t[0])
            .Concat(triangles.Select(t => t[1]))
            .Concat(triangles.Select(t => t[2]))
            .Chunk(3).ToArray();

        return (valid(triangles), valid(triangles2));
    }
}