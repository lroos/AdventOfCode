using System.Text.RegularExpressions;

public class Day14 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var template = input.First();

        var rules = input.Skip(2)
            .Select(line => line.Split(" -> "))
            .ToDictionary(x => x[0], x => x[1][0]);

        long calc(int steps)
        {
            var pairs = rules.ToDictionary(r => r.Key,
                r => new Regex(r.Key).Matches(template).LongCount());

            var chars = Enumerable.Range(0, 26).Select(i => (char)('A' + i))
                .ToDictionary(c => c, c => template.LongCount(t => t == c));

            for (int step = 0; step < steps; step++)
            {
                foreach (var rule in rules)
                    chars[rule.Value] += pairs[rule.Key];

                pairs = rules.ToDictionary(r => r.Key,
                    r => rules
                    .Where(x => x.Key.Insert(1, x.Value.ToString()).Contains(r.Key))
                    .Sum(x => pairs[x.Key]));
            }

            var elements = chars.Where(c => c.Value > 0).OrderBy(d => d.Value);
            return elements.Last().Value - elements.First().Value;
        }

        return (calc(10), calc(40));
    }
}