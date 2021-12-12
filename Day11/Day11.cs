public class Day11 : ISolution
{
    sealed record dumbo(int x, int y)
    {
        public int energy;
        public bool Equals(dumbo? other) => x == other!.x && y == other.y;
    };

    public (long, long) Run(string[] input)
    {
        var octos = input.SelectMany((line, y) => line.Select((d, x) => new dumbo(x, y) { energy = d - '0' }))
            .ToList();

        IEnumerable<dumbo> neighbours(dumbo d) => octos
            .Where(o => Math.Abs(d.x - o.x) < 2 && Math.Abs(d.y - o.y) < 2 && d != o);

        int flashes = 0, s100 = 0, last = 0;

        for (int step = 0; step < 300; step++)
        {
            foreach (var octo in octos)
            {
                octo.energy = octo.energy >= 10 ? 1 : octo.energy + 1;
            }

            var flashed = octos.Where(o => o.energy == 10).ToList();

            do
            {
                var adj = flashed
                    .SelectMany(f => neighbours(f).Where(n => n.energy < 10))
                    .Distinct().ToList();

                foreach (var o in adj)
                {
                    o.energy += neighbours(o)
                        .Intersect(flashed).Count();
                }

                flashed = adj.Where(o => o.energy >= 10).ToList();
            } while (flashed.Count > 0);

            var count = octos.Count(o => o.energy >= 10);
            flashes += count;
            if (step == 99) s100 = flashes;
            if (count == 100)
            {
                last = step + 1;
                break;
            };
        }

        return (s100, last);
    }
}