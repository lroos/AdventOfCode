public class Day22 : ISolution
{
    record Cube(int x1, int x2, int y1, int y2, int z1, int z2)
    {
        public long Width => x2 - x1 + 1;
        public long Height => y2 - y1 + 1;
        public long Depth => z2 - z1 + 1;
        public long Volume => Math.Max(0L, Width * Height * Depth) - Intersections.Sum(i => i.Volume);
        public bool Exists => Width > 0 && Height > 0 && Depth > 0;
        public List<Cube> Intersections = new();
        
        public void Intersect(Cube c)
        {
            var area = new Cube(Math.Max(x1, c.x1), Math.Min(x2, c.x2),
                Math.Max(y1, c.y1), Math.Min(y2, c.y2),
                Math.Max(z1, c.z1), Math.Min(z2, c.z2));

            if (!area.Exists) return;

            foreach (var i in Intersections)
                i.Intersect(area);

            Intersections.RemoveAll(i => i.Volume == 0);
            Intersections.Add(area);
        }

        public static Cube Parse(string input)
        {
            var v = input.Split(",").Select(pair => pair.Split("=")[1].Split("..")
                .Select(num => int.Parse(num)).ToArray()).ToArray();
            return new Cube(v[0][0], v[0][1], v[1][0], v[1][1], v[2][0], v[2][1]);
        }
    }

    public (long, long) Run(string[] input)
    {
        var steps = input.Select(l => l.Split(" ")).Select(x => (on: x[0] == "on", area: Cube.Parse(x[1]))).ToList();
        var cubes = new List<Cube> { steps[0].area };

        foreach (var step in steps.Skip(1))
        {
            for (var i = 0; i < cubes.Count; i++)        
                cubes[i].Intersect(step.area);
        
            if (step.on)
                cubes.Add(step.area);
        }

        var init = new Cube(-50, 50, -50, 50, -50, 50);

        return (0, cubes.Sum(c => c.Volume));
    }
}