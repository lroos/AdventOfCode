using System.Text.RegularExpressions;

public class Day15 : ISolution
{
    record point(int x, int y);

    public (long, long) Run(string[] input)
    {
        int x(int size)
        {
            var grid = input.Select(line => line.Select(d => int.Parse(d.ToString())).ToArray())
                .ToArray();

            grid = (from i in Enumerable.Range(0, size)
                    from row in grid
                    select Enumerable.Range(0, size)
                        .SelectMany(j => row.Select(n => ((n + i + j - 1) % 9) + 1))
                        .ToArray())
                   .ToArray();

            int w = grid[0].Length, h = grid.Length;
            IEnumerable<point> neighbours(point p) => from n in new[] { new point(p.x, p.y - 1), new point(p.x + 1, p.y), new point(p.x, p.y + 1), new point(p.x - 1, p.y) }
                                                      where n.x >= 0 && n.x < w && n.y >= 0 && n.y < h
                                                      select n;

            var paths = new[] { (point: new point(0, 0), risk: 0) };
            var min = new Dictionary<point, int>();

            do
            {
                var next = from p in paths
                           from point in neighbours(p.point)
                           let risk = p.risk + grid[point.y][point.x]
                           where !min.TryGetValue(point, out var m) || risk < m
                           select (point, risk);

                paths = next.GroupBy(n => n.point)
                    .Select(g => g.OrderBy(n => n.risk).First())
                    .ToArray();

                foreach (var end in paths)
                    min[end.point] = end.risk;

            } while (paths.Length > 0);

            return min[new point(w - 1, h - 1)];
        }

        return (x(1), x(5));
    }
    
}