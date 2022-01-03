public class Day02 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var ids = input.Select(x => x.GroupBy(c => c))
            .Select(x => (
                d2: x.FirstOrDefault(g => g.Count() == 2),
                d3: x.FirstOrDefault(g => g.Count() == 3)));

        var checksum = ids.Count(id => id.d2 != null) * ids.Count(id => id.d3 != null);

        var test = input.Select(box => (id: box, diffs: input.Select(box2 => box2.Select((c, i) => c - box[i]).ToArray()).ToArray()))
            .Where(box => box.diffs.Count(diff => diff.Count(x => x != 0) == 1) == 1)
            .ToList();
        var letters = test[0].id.Where((c, i) => test[1].id[i] == c).ToArray();

        Console.WriteLine(new String(letters));
        return (checksum, 0);
    }
}