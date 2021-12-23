public class Day20 : ISolution
{
    record pixel(int x, int y, bool lit);

    public (long, long) Run(string[] input)
    {
        var index = input.First();

        int run(int steps)
        {
            var image = input.Skip(2)
                .SelectMany((line, y) => line.Select((c, x) => new pixel(x, y, c == '#')))
                .ToArray();
            int min = 0, max = input.Skip(2).Count() - 1;

            pixel zoom(int x, int y)
            {
                var bits = image.Where(p => Math.Abs(x - p.x) < 2 && Math.Abs(y - p.y) < 2)
                    .OrderBy(p => p.y).ThenBy(p => p.x).Select(p => p.lit ? '1' : '0');

                return new pixel(x, y, index[Convert.ToInt32(string.Join("", bits), 2)] == '#');
            }

            IEnumerable<pixel> getBorder(int min, int max, bool lit, int size = 2) =>
                from y in Enumerable.Range(min - size, max - min + size * 2 + 1)
                from x in Enumerable.Range(min - size, max - min + size * 2 + 1)
                where x <= min || x >= max || y <= min || y >= max
                select new pixel(x, y, lit);

            image = image.Union(getBorder(min - 1, max + 1, false)).ToArray();

            for (var step = 0; step < steps; step++)
            {
                var lit = image.Where(p => p.x == p.y && p.y == min - 2).First().lit;
                image = image.Union(getBorder(--min - 2, ++max + 2, lit)).ToArray();

                image = (from y in Enumerable.Range(min - 2, max - min + 5)
                         from x in Enumerable.Range(min - 2, max - min + 5)
                         select zoom(x, y))
                           .ToArray();
            }

            return image.Count(p => p.lit);
        }

        return (run(2), run(50));
    }
}