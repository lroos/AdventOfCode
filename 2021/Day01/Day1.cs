public class Day1 : ISolution
{
    (long, long) ISolution.Run(string[] input)
    {
        var data = input.Select(line => Convert.ToInt32(line)).ToList();
        return (Part1(data), Part2(data));
    }

    int Part1(IEnumerable<int> data)
    {
        var previous = (int?)null;
        var count = 0;

        foreach (var depth in data)
        {
            if (previous.HasValue && depth > previous) count++;
            previous = depth;
        }

        return count;
    }

    int Part2(IList<int> data)
    {
        var previous = (int?)null;
        var count = 0;

        for (int i = 0; i < data.Count; i++)
        {
            if (i < 2) continue;

            var depth = data[i] + data[i - 1] + data[i - 2];

            if (previous.HasValue && depth > previous) count++;
            previous = depth;
        }

        return count;
    }
}


