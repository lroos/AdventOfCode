public class Day02 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var keypad = new char[,] { 
            { '1', '2', '3' }, 
            { '4', '5', '6' }, 
            { '7', '8', '9' } };
        var keypad2 = new char[,] { 
            { '0', '0', '1', '0', '0' }, 
            { '0', '2', '3', '4', '0' }, 
            { '5', '6', '7', '8', '9' }, 
            { '0', 'A', 'B', 'C', '0' }, 
            { '0', '0', 'D', '0', '0' } };

        string getCode(char[,] keypad, (int x, int y) finger)
        {
            var digits = new List<char>();
            int h = keypad.GetLength(0), w = keypad.GetLength(1);

            foreach (var digit in input)
            {
                foreach (var step in digit)
                {
                    var x = finger.x + step switch { 'L' => -1, 'R' => 1, _ => 0 };
                    var y = finger.y + step switch { 'U' => -1, 'D' => 1, _ => 0 };

                    var noop = y < 0 || y >= h || x < 0 || x >= w || keypad[y, x] == '0';
                    finger = noop ? finger : (x, y);
                }

                digits.Add(keypad[finger.y, finger.x]);
            }

            return string.Join(string.Empty, digits);
        }
        
        Console.WriteLine($"({getCode(keypad, (1, 1))}, {getCode(keypad2, (2, 0))})");
        return (0, 0);
    }
}