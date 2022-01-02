public class Day01 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var result = input.Aggregate(0, (frequency, change) => frequency + int.Parse(change));

        int frequency = 0;
        int? twice = null;
        var visited = new HashSet<int>();        

        for (var i = 0; true; i++)
        {
            frequency += int.Parse(input[i % input.Length]);
            if (twice == null && visited.Contains(frequency))
            {
                twice = frequency;
                break;
            }
            else
            {
                visited.Add(frequency);
            }
        }

        return (result, twice.Value);
    }
}