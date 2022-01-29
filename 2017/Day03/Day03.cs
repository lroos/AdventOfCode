public class Day03 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var num = int.Parse(input[0]);

        int i = 1, z = 1, sides = 1;
        var point = (x: 0, y: 0, z: i);
        var points = new List<(int x, int y, int z)> { point };
        var points2 = new List<(int x, int y, int z)> { point };
        
        do
        {
            foreach (var change in new[] { (1, 0), (0, -1), (-1, 0), (0, 1) }.ToArray())
                foreach (var step in Enumerable.Range(0, ++sides / 2))
                {
                    point = (point.x + change.Item1, point.y + change.Item2, ++i);
                    points.Add(point);
                    if (z < num)
                    {
                        z = points2
                            .Where(p => Math.Abs(p.x - point.x) < 2 && Math.Abs(p.y - point.y) < 2)
                            .Sum(p => p.z);
                        points2.Add((point.x, point.y, z));
                    }
                }
        }
        while (i < num);

        var last = points.AsEnumerable().Reverse().First(p => p.z == num);
        var next = points2.First(p => p.z > num);

        return (Math.Abs(last.x) + Math.Abs(last.y), next.z);
    }
}