public class Day2 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var sub = new Submarine();
        foreach (var item in input)
        {
            var parts = item.Split(" ");
            var command = parts[0];
            var amount = Convert.ToInt32(parts[1]);
            sub.Go(command, amount);
        }

        return (0, sub.Depth * sub.Distance);
    }

    public class Submarine
    {
        public int Pitch { get; set; } = 0;
        public int Depth { get; set; } = 0;
        public int Distance { get; set; } = 0;

        public void Go(string command, int amount)
        {
            switch (command)
            {
                case "up":
                    Aim(-amount);
                    break;
                case "down":
                    Aim(amount);
                    break;
                case "forward":
                    Forward(amount);
                    break;
            }
        }

        public void Aim(int amount)
        {
            Pitch += amount;
        }

        public void Dive(int amount)
        {
            Depth = Math.Max(Depth + amount, 0);
        }

        public void Forward(int amount)
        {
            Distance += amount;
            Dive(Pitch * amount);
        }
    }
}