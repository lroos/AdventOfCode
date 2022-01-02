public class Day01 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var expenses = input.Select(x => int.Parse(x)).ToHashSet();
        int remainder(int target) => expenses.FirstOrDefault(x => expenses.Contains(target - x));

        var result = remainder(2020);
        var result2 = expenses.FirstOrDefault(x => remainder(2020 - x) > 0);
        var result3 = remainder(2020 - result2);

        return (result * (2020 - result), result2 * result3 * (2020 - result2 - result3));
    }
}