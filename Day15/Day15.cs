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

            var paths = new[] { new LinkedList<(point point, int risk)>(new[] { (new point(0, 0), 0) }) };
            var finished = new List<LinkedList<(point point, int risk)>>();
            var min = new Dictionary<point, int>();

            do
            {
                var next = from p in paths
                           let last = p.Last!.Value
                           from n in neighbours(last.point)
                           let risk = last.risk + grid[n.y][n.x]
                           where !min.TryGetValue(n, out var m) || risk < m
                           select new LinkedList<(point point, int risk)>(p.Append((n, risk)));

                paths = next.GroupBy(n => n.Last!.Value.point)
                    .Select(g => g.OrderBy(n => n.Last!.Value.risk).First())
                    .ToArray();

                foreach (var end in paths.Select(p => p.Last.Value))
                    min[end.point] = end.risk;

                finished.AddRange(paths.Where(p => p.Last.Value.point == new point(w - 1, h - 1)));

            } while (paths.Length > 0);

            return finished.OrderBy(f => f.Last.Value.risk).First().Last.Value.risk;
        }

        return (x(1), x(5));
    }
    
}