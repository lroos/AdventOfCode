/// <summary>
/// 
/// </summary>
public class Day9
{
    record point(int x, int y);

    public (int, int) Run(string[] input)
    {
        var grid = input.Select(line => line.ToArray().Select(d => int.Parse(d.ToString())).ToArray())
            .ToArray();
        int w = grid[0].Length, h = grid.Length;
        var visited = new bool[w,h];

        IEnumerable<point> neighbours(point p) => from n in new[] { new point(p.x, p.y - 1), new point(p.x + 1, p.y), new point(p.x, p.y + 1), new point(p.x - 1, p.y) }
                                                  where n.x >= 0 && n.x < w && n.y >= 0 && n.y < h
                                                  select n;

        var basins = from x in Enumerable.Range(0, w)
                     from y in Enumerable.Range(0, h)
                     let point = new point(x, y)
                     where neighbours(point).All(n => grid[y][x] < grid[n.y][n.x])
                     select point;

        var risk = basins.Sum(p => grid[p.y][p.x] + 1);

        int sizeOf(point p) 
        {
            visited[p.x, p.y] = true;
            var depth = grid[p.y][p.x];            
            return 1 + neighbours(p)
                .Where(n => !visited[n.x, n.y]
                    && grid[n.y][n.x] >= depth && grid[n.y][n.x] < 9)
                .Sum(n => sizeOf(n));
        };

        var largest = basins.Select(b => sizeOf(b))
            .OrderByDescending(size => size)
            .Take(3);

        var size = largest
            .Aggregate(1, (size, acc) => acc * size);

        return (risk, size);
    }
}