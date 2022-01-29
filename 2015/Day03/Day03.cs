public class Day03 : ISolution
{
    public (long, long) Run(string[] input)
    {
       var compass = new[]
       {
            ('^', 0, 1),
            ('>', 1, 0),
            ('v', 0, -1),
            ('<', -1, 0)
        }.ToDictionary(x => x.Item1, x => (x: x.Item2, y: x.Item3));

        int deliver(int santas)
        {
            var coords = new (int x, int y)[santas];
            var visited = new HashSet<(int, int)>(coords);

            foreach (var step in input[0].Chunk(santas))
                for (int i = 0; i < santas; i++)
                {
                    coords[i] = (coords[i].x + compass[step[i]].x, coords[i].y + compass[step[i]].y);
                    visited.Add(coords[i]);
                }

            return visited.Count;
        };

        return (deliver(1), deliver(2));
    }
}