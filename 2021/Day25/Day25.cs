public class Day25 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var spots2 = input.Select(l => l.ToArray()).ToArray();
        char[][] spots;
        int w = spots2[0].Length, h = spots2.Length;
        bool change;
        int step = 0;

        do
        {
            step++;
            change = false;
            spots = spots2.Select(s => (char[])s.Clone()).ToArray();
            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    var cs = spots[y][x];
                    var ps = spots[y][(x + w - 1) % w];
                    var ns = spots[y][(x + 1) % w];

                    spots2[y][x] = cs switch
                    {
                        '.' => ps == '>' ? '>' : cs,
                        '>' => ns == '.' ? '.' : cs,
                        _ => cs
                    };

                    change = change || spots2[y][x] != cs;
                }

            spots = spots2.Select(s => (char[])s.Clone()).ToArray();
            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                {
                    var cs = spots[y][x];
                    var ps = spots[(y + h - 1) % h][x];
                    var ns = spots[(y + 1) % h][x];

                    spots2[y][x] = cs switch
                    {
                        '.' => ps == 'v' ? 'v' : cs,
                        'v' => ns == '.' ? '.' : cs,
                        _ => cs
                    };

                    change = change || spots2[y][x] != cs;
                }            
        }
        while (change);

        return (step, 0);
    }
}