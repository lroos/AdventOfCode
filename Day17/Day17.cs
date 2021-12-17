using System.Text.RegularExpressions;

public class Day17 : ISolution
{       
    public (long, long) Run(string[] input)
    {
        var range = @"=(-?\d+)\.\.(-?\d+)";
        var cap = new Regex($"x{range}, y{range}").Match(input[0]).Groups.Values.Skip(1)
            .Select(c => int.Parse(c.Value)).ToArray();
        var area = (x1: cap[0], y1: cap[3], x2: cap[1], y2: cap[2]);

        IEnumerable<(int x, int dx)> values(int dx, int? min, int x = 0)
        {
            while (true) yield return (x += dx, dx -= dx == min ? 0 : 1);
        }

        var paths = from h in Enumerable.Range(1, area.x2 + 1).Reverse()
                    from v in Enumerable.Range(area.y2, area.y2 * -2 + 1)
                    let path = values(h, 0)
                        .Zip(values(v, null))
                        .TakeWhile(x => (x.First.dx > 0 || x.First.x >= area.x1)
                            && x.First.x <= area.x2 && x.Second.x >= area.y2)
                    where path.Any(x => x.First.x >= area.x1 && x.Second.x <= area.y1)
                    select path;
        
        return (paths.Max(p => p.Max(x => x.Second.x)), paths.Count());
    }    
}