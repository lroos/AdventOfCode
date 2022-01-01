public class Day21 : ISolution
{
    record Player()
    {
        public int Pos;
        public int Score = 0;
    }

    public (long, long) Run(string[] input)
    {
        var players = input.Select(l => int.Parse(l.Last().ToString()))
            .Select(p => new Player() { Pos = p }).ToArray();

        IEnumerable<Player> players2()
        {
            while (true)
                foreach (var p1 in players) yield return p1;
        };

        int Score(int pos) => (pos - 1) % 10 + 1;

        int deterministic()
        {
            int dieRolls = 0;

            IEnumerable<int> rolls()
            {
                while (true)
                    yield return ((++dieRolls - 1 % 100) + 1);
            };

            foreach (var player in players2())
            {
                player.Pos += rolls().Take(3).Sum();
                player.Score += Score(player.Pos);
                if (player.Score >= 1000)
                    break;
            }

            return players.Min(p => p.Score) * dieRolls;
        }

        IEnumerable<(int roll, int prob)> Outcomes() => new[] { (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1) };

        (long p1, long p2) Play((int pos, int score) p1, (int pos, int score) p2, long universes)
        {
            long w1 = 0, w2 = 0;
            
            foreach (var outcome in Outcomes())
            {                
                var pos = p1.pos + outcome.roll;
                var score = p1.score + Score(pos);

                if (score >= 21)
                {
                    w1 += universes * outcome.prob;
                }
                else
                {                    
                    var scores = Play(p2, (pos, score), universes * outcome.prob);
                    w1 += scores.p2;
                    w2 += scores.p1;
                }
            }

            return (w1, w2);
        }

        var wins = Play((players[0].Pos, 0), (players[1].Pos, 0), 1);

        return (deterministic(), Math.Max(wins.p1, wins.p2));
    }
}