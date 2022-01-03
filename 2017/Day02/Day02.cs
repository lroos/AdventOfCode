public class Day02 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var sheet = input.Select(x => x.Split('\t').Select(x => int.Parse(x)).ToArray()).ToArray();
        var checksum = sheet.Aggregate(0, (sum, row) => sum + row.Max() - row.Min());
        var sum = sheet.Sum(row => row.Select(x => (x, d: row.FirstOrDefault(i => i != x && x % i == 0)))
            .Where(x => x.d != 0).Select(x => x.x / x.d).First());

        return (checksum, sum);
    }
}