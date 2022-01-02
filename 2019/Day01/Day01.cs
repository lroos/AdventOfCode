public class Day01 : ISolution
{
    public (long, long) Run(string[] input)
    {
        int moreFuel(int mass) => mass >= 8 ? mass / 3 - 2 + moreFuel(mass / 3 - 2) : 0;

        var result = input.Aggregate(0, (fuel, mass) => fuel + int.Parse(mass) / 3 - 2);
        var totalFuel = input.Aggregate(0, (fuel, mass) => fuel + moreFuel(int.Parse(mass)));
        
        return (result, totalFuel);
    }
}