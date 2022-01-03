public class Day02 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var passwords = input.Select(x => x.Split("-: ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            .Select(x => (min: int.Parse(x[0]), max: int.Parse(x[1]), letter: x[2][0], pwd: x[3]))
            .ToArray();

        bool policy1((int min, int max, char letter, string pwd) x)
            => x.pwd.Count(c => c == x.letter) >= x.min && x.pwd.Count(c => c == x.letter) <= x.max;
        bool policy2((int min, int max, char letter, string pwd) x)
            => new[] { x.pwd[x.min - 1], x.pwd[x.max - 1] }.Count(c => c == x.letter) == 1;

        return (passwords.Count(policy1), passwords.Count(policy2));
    }
}