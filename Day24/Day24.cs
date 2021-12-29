public class Day24 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var sets = input.Chunk(18).SelectMany(c => new[] { c[4], c[5], c[15] })
            .Select(c => Convert.ToInt32(c.Substring(6))).Chunk(3)
            .Select(c => (a: c[0], b: c[1], c: c[2]))
            .ToArray();

        var nums = Enumerable.Range(1, 9);

        IEnumerable<long> check(int[] digits, int i, int z)
        {
            if (digits.Length == sets.Length) return z == 0 ? new[] { long.Parse(String.Join("", digits)) } : new long[0];

            IEnumerable<long> sub(IEnumerable<int> range, Func<int, int> z) =>
                range.SelectMany(w => check(digits.Append(w).ToArray(), i + 1, z(w)));

            return sets[i].b < 0
                ? sub(nums.Where(n => z % 26 + sets[i].b == n), (w) => z / 26)
                : sub(nums, (w) => z * 26 + w + sets[i].c);
        }

        var serials = check(new int[0], 0, 0);
       
        return (serials.Min(), serials.Max());
    }
}