public class Day23 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var remix = input.ToList();
        remix.InsertRange(3, new[]
        {
            "  #D#C#B#A#",
            "  #D#B#A#C#"
        });
        input = remix.ToArray();
        var capacity = input.Length - 3;

        var pods = new Stack<char[]>((from col in new[] { 9, 7, 5, 3 }
                                     from row in Enumerable.Range(0, capacity)
                                     select input[5 - row][col]).Chunk(capacity));

        var spaces = "HHAHBHCHDHH".Select(c => (Type: c, Capacity: c == 'H' ? 1: capacity)).ToArray();
        var initial = spaces.Select(x => x.Type == 'H' ? new char[0] : pods.Pop()).ToArray();

        IEnumerable<(int from, int to, int cost)> moves(Stack<char>[] state)
        {
            int steps(int from, int to) => (spaces[from].Type == 'H' ? 0 : (spaces[from].Capacity + 1 - state[from].Count))
                + Math.Abs(to - from)
                + (spaces[to].Type == 'H' ? 0 : (spaces[to].Capacity - state[to].Count));
            (int, int, int)move(int from, int to) => (from, to, (int)Math.Pow(10, state[from].Peek() - 'A') * steps(from, to));                  
            
            var rooms = state.Select((room, i) => (room, i, space: spaces[i])).ToLookup(x => x.space.Type == 'H' ? 'H' : 'R');
            bool reachable(int from, int to) => !rooms['H'].Any(o => o.i > Math.Min(from, to) && o.i < Math.Max(from, to) && o.room.Count > 0);
            bool tryReturn(int from, out int to)
            {                
                to = rooms['R'].First(r => r.space.Type == state[from].Peek()).i;
                return reachable(to, from) && !state[to].Any(c => c != state[from].Peek());                
            }

            foreach (var leaving in rooms['R'].Where(r => r.room.Any(c => c != r.space.Type)))
                if (tryReturn(leaving.i, out var to))
                {
                    yield return move(leaving.i, to);
                }
                else
                {
                    foreach (var hallway in rooms['H'].Where(h => h.room.Count == 0 && reachable(leaving.i, h.i)))
                        yield return move(leaving.i, hallway.i);
                }

            foreach (var leaving in rooms['H'].Where(h => h.room.Any()))
                if (tryReturn(leaving.i, out var to))
                    yield return move(leaving.i, to);
        }

        var costs = new HashSet<int>();

        void play(char[][] state, int cost)
        {
            if (state.Zip(spaces).Where(x => x.Second.Type != 'H')
                .All(x => x.First.Length == x.Second.Capacity && x.First.All(c => c == x.Second.Type)))
            {
                costs.Add(cost);
                return;
            }

            var rooms = state.Select(s => new Stack<char>(s)).ToArray();

            foreach (var move in moves(rooms))
            {
                rooms[move.to].Push(rooms[move.from].Pop());                
                play(rooms.Select(l => l.Reverse().ToArray()).ToArray(), cost + move.cost);
                rooms[move.from].Push(rooms[move.to].Pop());
            }
        }

        play(initial, 0);

        return (0, costs.Min());
    }
}