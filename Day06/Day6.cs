public class Day6 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var initial = input.First().Split(",").Select(x => int.Parse(x)).ToArray();        
        return (PopulationAfter(80, initial), PopulationAfter(256, initial));
    }

    long PopulationAfter(int days, int[] initial)
    {
        long[] newbirths = new long[days];

        IEnumerable<int> births(int initialAge, int daysLeft) => Enumerable.Range(0, 50)
            .Select(x => x * 7 + initialAge)
            .Where(x => x < daysLeft);

        foreach (var age in initial)
        {
            foreach (var birthday in births(age, days))
            {
                newbirths[birthday] += 1;
            }
        }

        for (int day = 0; day < days; day++)
        {
            var newBorn = newbirths[day];

            foreach (var birthday in births(8, days - day - 1))
            {
                newbirths[day + 1 + birthday] += newBorn;
            }
        }

        return initial.Length + newbirths.Sum();
    }
}