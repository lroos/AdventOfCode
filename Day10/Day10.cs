public class Day10
{
    record score(int error, long autocomplete);
    
    public (int, long) Run(string[] input)
    {
        var pairs = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        var errorCodes = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        var brackets = ")]}>";

        score parse(string line)
        {
            var stack = new Stack<char>();
            foreach (var bracket in line)
            {
                if (pairs.ContainsKey(bracket))
                {
                    stack.Push(bracket);
                }
                else if (bracket == pairs[stack.Peek()])
                {
                    stack.Pop();
                }
                else
                {
                    return new score(errorCodes[bracket], 0);
                }
            }

            var closing = new string(stack.Select(bracket => pairs[bracket]).ToArray());
            return new score(0, closing.Aggregate(0L, (score, bracket) => score * 5 + (brackets.IndexOf(bracket) + 1)));
        }

        var scores = input.Select(line => parse(line));
        var errors = scores.Sum(score => score.error);
        var autocompletes = scores.Where(score => score.error == 0)
            .OrderBy(score => score.autocomplete).ToArray();

        var median = autocompletes[autocompletes.Length / 2].autocomplete;

        return (errors, median);
    }
}