using System.Text;

public class Day13 : ISolution
{   
    public (long, long) Run(string[] input)
    {
        var folds = input.Where(line => line.StartsWith("fold"))
                .Select(line => line.Substring(11).Split("="))
                .Select(fold => (fold[0][0], int.Parse(fold[1])));

        int min(char way) => folds.Where(fold => fold.Item1 == way).Min(fold => fold.Item2) + 1;
        var width = min('x');
        var height = min('y');

        int translate(int pos, int size) => ((pos / size) % 2 == 0
                    ? (pos + 1) % size
                    : size - ((pos + 1) % size)) - 1;

        var dots = input.TakeWhile(line => !string.IsNullOrEmpty(line))
            .Select(line => line.Split(",").Select(x => int.Parse(x)).ToArray())
            .Select(xy => (translate(xy[0], width), translate(xy[1], height)));

        var offset = Console.GetCursorPosition();
        foreach (var dot in dots)
        {
            Console.SetCursorPosition(dot.Item1, dot.Item2 + offset.Top);
            Console.Write('#');
        }
        Console.SetCursorPosition(0, offset.Top + 6);

        return (0, 0);
    }
}