using System.Numerics;

public class Day20 : ISolution
{  
    record pixel(int x, int y, bool lit);

    public (long, long) Run(string[] input)
    {
        var index = input.First();
        var image = input.Skip(2)
            .SelectMany((line, y) => line.Select((c, x) => new pixel(x, y, c == '#')))
            .ToArray();
        int min = 0, max = input.Skip(2).Count() - 1;

        IEnumerable<pixel> zoom(int x, int y)
            => image.Where(p => Math.Abs(x - p.x) < 2 && Math.Abs(y - p.y) < 2);

        IEnumerable<pixel> getMask(int min, int max)
            => from y in Enumerable.Range(min - 1, max - min + 3)
               from x in Enumerable.Range(min - 1, max - min + 3)
               where x <= min || x >= max || y <= min || y >= max
               select new pixel(x, y, false);

        for (var step = 0; step < 2; step++)
        {
            image = image.Union(getMask(--min, ++max)).ToArray();

            image = (from y in Enumerable.Range(min, max - min + 1)
                     from x in Enumerable.Range(min, max - min + 1)
                     let key = Convert.ToInt32(string.Join("", zoom(x, y).Select(p => p.lit ? '1' : '0')), 2)
                     select new pixel(x, y, index[key] == '#'))
                       .ToArray();


            foreach (var line in image.OrderBy(p => p.y).ThenBy(p => p.x).GroupBy(p => p.y)
                .Select(g => string.Join("", g.Select(p => p.lit ? "#" : "."))))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();

        }

        var lit = image.Count(p => p.lit);

        return (lit, 0);
    }
}