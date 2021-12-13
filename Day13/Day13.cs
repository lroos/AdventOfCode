using System.Text;

public class Day13 : ISolution
{   
    public (long, long) Run(string[] input)
    {
        var folds = input.Where(line => line.StartsWith("fold"))
                .Select(line => line.Substring(11).Split("="))
                .Select(fold => (fold[0][0], int.Parse(fold[1])));

        int min(char way)
        {
            var steps = folds.Where(fold => fold.Item1 == way);
            return steps.Any() ? steps.Min(fold => fold.Item2) + 1 : int.MaxValue;
        }
        var width = min('x');
        var height = min('y');

        int translate(int pos, int size) => ((pos / size) % 2 == 0
                    ? (pos + 1) % size
                    : size - ((pos + 1) % size)) - 1;

        var dots = input.TakeWhile(line => !string.IsNullOrEmpty(line))
            .Select(line => line.Split(",").Select(x => int.Parse(x)).ToArray())
            .Select(xy => (translate(xy[0], width), translate(xy[1], height)))
            .Where(xy => xy.Item1 > -1 && xy.Item2 > -1)
            .Distinct();

        var canvas = new char[width, height];

        foreach (var dot in dots)
        {
            canvas[dot.Item1, dot.Item2] = '#';
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width ; x++)
                Console.Write(canvas[x, y] == 0 ? ' ' : canvas[x, y]);
            Console.WriteLine();
        }

        return (0, 0);
    }
}