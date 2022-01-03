public class Day02 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var presents = input.Select(l => l.Split('x').Select(x => int.Parse(x)).ToArray())
            .Select(x => (l: x[0], w: x[1], h: x[2], t: x[0] * x[1], f: x[1] * x[2], s: x[2] * x[0]));

        var area = presents.Aggregate(0, (area, d) => 
            area + 2 * d.t + 2 * d.f + 2 * d.s + Math.Min(Math.Min(d.t, d.f), d.s));
        var ribbon = presents.Aggregate(0, (len, d) =>
            len + 2 * Math.Min(Math.Min(d.l + d.w, d.w + d.h), d.h + d.l) + d.l * d.w * d.h);

        return (area, ribbon);
    }
}