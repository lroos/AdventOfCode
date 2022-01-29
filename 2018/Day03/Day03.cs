public class Day03 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var fabric = new int[1000,1000];
        
        var claims = input.Select(x => x.Split("#@,:x ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray())
            .Select(x => (id: x[0], x: x[1], y: x[2], w: x[3], h: x[4]))
            .ToArray();

        var overlapping = new HashSet<int>();

        foreach (var claim in claims)
            for (int x = claim.x; x <= claim.x + claim.w - 1; x++)
                for (int y = claim.y; y <= claim.y + claim.h - 1; y++)
                {
                    if (fabric[x, y] != 0)
                    {
                        overlapping.Add(fabric[x, y]);
                        overlapping.Add(claim.id);
                        fabric[x, y] = -claim.id;
                    } else 
                    {
                        fabric[x, y] = claim.id;
                    }                    
                }

        var overlap = fabric.OfType<int>().Count(p => p < 0);
        var safe = claims.Select(c => c.id).Except(overlapping).First();

        return (overlap, safe);
    }
}