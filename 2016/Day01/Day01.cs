public class Day01 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var compass = new[]
        {
            ('N', 0, 1),
            ('E', 1, 0),
            ('S', 0, -1),
            ('W', -1, 0)
        };

        int x = 0, y = 0, bearing = 100;
        var points = new HashSet<(int, int)>();
        (int x, int y)? HQ = null;

        foreach (var step in input[0].Split(", "))
        {
            bearing += (step[0] == 'R' ? 1 : -1);
            var dir = compass[bearing % 4];
            var distance = int.Parse(step.Substring(1));

            for (var i = 0; i < distance; i++)
            {
                x += dir.Item2;
                y += dir.Item3;

                if (HQ == null && points.Contains((x, y)))
                {
                    HQ = (x, y);
                }

                points.Add((x, y));
            }
        }

        return (x + y, HQ.Value.x + HQ.Value.y);
    }
}