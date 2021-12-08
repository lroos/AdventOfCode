/// <summary>
/// 
/// </summary>
public class Day8
{
    public (int, int) Run(string[] input)
    {
        string[] parse(string part, int pos) => part.Split(" | ")
            .ElementAt(pos)
            .Split(" ").Select(x => new string(x.OrderBy(c => c).ToArray()))
            .ToArray();

        var displays = input.Select(line => new
        {
            digits = parse(line, 0),
            output = parse(line, 1),
            decoded = new List<int>()
        }).ToList();

        var masks = new Dictionary<string, int>() {
            { "abcefg", 0 },
            { "cf", 1 },
            { "acdeg", 2 },
            { "acdfg", 3 },
            { "bcdf", 4 },
            { "abdfg", 5 },
            { "abdefg", 6 },
            { "acf", 7 },
            { "abcdefg", 8 },
            { "abcdfg", 9 },
        };

        foreach (var display in displays)
        {
            var cipher = GetCipher(display.digits);
            var plain = display.output.Select(ciphertext => new string(ciphertext.Select(c => cipher[c]).OrderBy(c => c).ToArray()));
            display.decoded.AddRange(plain.Select(mask => masks[mask]));
        }

        var easy = displays.Sum(x => x.decoded.Count(i => new[] { 1, 4, 7, 8 }.Contains(i)));
        var hard = displays.Select(x => int.Parse(string.Join("", x.decoded.Select(digit => digit.ToString())))).Sum();

        return (easy, hard);
    }

    Dictionary<char, char> GetCipher(string[] input)
    {
        var chars = "abcdefg".ToArray();

        var d1 = input.Where(i => i.Length == 2).First();
        var d4 = input.Where(i => i.Length == 4).First();
        var d7 = input.Where(i => i.Length == 3).First();

        var map = new Dictionary<char, char>();
        var a = d7.Except(d1);
        map[a.First()] = 'a';
        var b = chars.Where(c => input.Count(i => i.Contains(c)) == 6);
        map[b.First()] = 'b';
        map[chars.Where(c => input.Count(i => i.Contains(c)) == 8).Except(a).First()] = 'c';
        var d = d4.Except(d1).Except(b);
        map[d.First()] = 'd';
        map[chars.Where(c => input.Count(i => i.Contains(c)) == 4).First()] = 'e';
        map[chars.Where(c => input.Count(i => i.Contains(c)) == 9).First()] = 'f';
        map[chars.Where(c => input.Count(i => i.Contains(c)) == 7).Except(d).First()] = 'g';

        return map;        
    }
}