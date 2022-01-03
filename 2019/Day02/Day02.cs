public class Day02 : ISolution
{
    public (long, long) Run(string[] input)
    {
        int run(int noun, int verb)
        {
            var program = input[0].Split(",").Select(x => int.Parse(x)).ToArray();
            program[1] = noun;
            program[2] = verb;

            try
            {
                for (int i = 0; i < program.Length; i++)
                {
                    var op = program[i];

                    if (op == 99) break;
                    if (i + 3 >= program.Length) break;
                    int p1 = program[i + 1], p2 = program[i + 2], r1 = program[i + 3];
                    if (Math.Max(Math.Max(p1, p2), r1) > program.Length) break;

                    var operands = (program[p1], program[p2]);
                    program[r1] = op switch
                    {
                        1 => operands.Item1 + operands.Item2,
                        2 => operands.Item1 * operands.Item2,
                        _ => throw new InvalidOperationException()
                    };

                    i += 3;
                }
            }
            catch { }

            return program[0];
        }

        int noun = 0, verb = 0;
        var result1 = run(12, 2);

        foreach (var n in Enumerable.Range(0, 100))
        {
            foreach (var v in Enumerable.Range(0, 100))
                if (run(n, v) == 19690720)
                {
                    noun = n;
                    verb = v;
                    break;
                }
            if (noun > 0)
                break;
        }

        return (result1, noun * 100 + verb);
    }
}