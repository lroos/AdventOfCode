using System.Diagnostics;
/// <summary>
/// Overly verbose
/// </summary>
public class Day4
{       
    public (int, int) Run(string[] input)
    {
        var draws = input[0]
            .Split(",")
            .Select(x => Convert.ToInt32(x))
            .ToList();

        var boards = LoadBoards(input.Skip(2));

        return PlayBingo(boards, draws);
    }   

    IList<BingoBoard> LoadBoards(IEnumerable<string> lines)
    {
        var boards = new List<BingoBoard>();

        while (lines.Any())
        {
            boards.Add(BingoBoard.Create(lines.Take(5)));
            lines = lines.Skip(6);
        }

        return boards;
    }

    (int, int) PlayBingo(IList<BingoBoard> players, IEnumerable<int> draws)
    {
        var first = (int?)null;
        var last = (int?)null;

        foreach (var draw in draws)
        {
            foreach (var board in players)
            {
                board.Marked.Add(draw);
            }

            var winner = players.FirstOrDefault(board => board.IsBingo);

            if (winner != null)
            {
                var score = winner.GetScore(draw);

                if (!first.HasValue)
                {
                    first = score;
                }

                if (!last.HasValue && !players.Any(board => !board.IsBingo))
                {
                    last = score;
                    break;
                }
            }

            players = players
                .Where(board => !board.IsBingo)
                .ToList();
        }

        return (first.Value, last.Value);
    }

    class BingoBoard
    {
        int[][] State;

        BingoBoard(int[][] numbers)
        {
            State = numbers;
        }

        public static BingoBoard Create(IEnumerable<string> rows)
        {
            Debug.Assert(rows.Count() == 5);

            var state = rows
                .Select(n => n.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Convert.ToInt32(s))
                    .ToArray())
                .ToArray();

            return new BingoBoard(state);
        }

        public IEnumerable<int[]> Rows
        {
            get
            {
                return State;
            }
        }

        public IEnumerable<int[]> Columns
        {
            get
            {
               for (int column = 0; column < State[0].Length; column++)
                {
                    yield return State
                        .Select(row => row[column])
                        .ToArray();
                }
            }
        }

        public List<int> Marked { get; set; } = new();

        public IEnumerable<int> AllNumbers
        {
            get
            {
                return State.SelectMany(row => row);
            }
        }

        public bool IsBingo
        {
            get
            {
                return Rows.Any(row => IsBingoLine(row)) ||
                    Columns.Any(col => IsBingoLine(col));
            }
        }

        bool IsBingoLine(int[] line)
        {
            return line.All(num => Marked.Contains(num));
        }

        public void Reset()
        {
            Marked.Clear();
        }

        public int GetScore(int called)
        {
            return AllNumbers.Except(Marked).Sum() * called;
        }
    }
}