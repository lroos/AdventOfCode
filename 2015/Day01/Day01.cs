public class Day01 : ISolution
{
    public (long, long) Run(string[] input)
    {
        int floor = 0;
        int? basement = null;
        for (int i = 0; i < input[0].Length; i++)
        {
            floor += (input[0][i] == '(' ? 1 : -1);
            
            if (basement == null && floor == -1)
            {
                basement = i + 1;
            }
        }

        return (floor, basement.Value);
    }
}