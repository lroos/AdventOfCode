public class Day18 : ISolution
{
    public class snail
    {
        public snail(int value, int level)
        {
            Value = value;
            Level = level;
        }

        public int Value { get; set; }
        public int Level { get; init; }
    }

    public (long, long) Run(string[] input)
    {
        IEnumerable<snail> Parse(string line)
        {
            int level = 0;            
            foreach (var c in line)
            {
                level += c == '[' ? 1 : c == ']' ? -1 : 0;
                if (char.IsDigit(c))
                    yield return new snail(int.Parse(c.ToString()), level);
            }
        }

        LinkedListNode<snail> Collapse(LinkedList<snail> num, LinkedListNode<snail> node, int value)
        {
            var newNode = new LinkedListNode<snail>(new snail(value, node.Value.Level - 1));
            num.AddBefore(node, newNode);
            num.Remove(node.Next);
            num.Remove(node);
            return newNode;
        }

        LinkedList<snail> Reduce(LinkedList<snail> num)
        {
            snail snail;
            do
            {
                snail = num.FirstOrDefault(n => n.Level > 4);

                if (snail != null)
                {
                    var node = num.Find(snail);
                    if (node.Previous != null)
                        node.Previous.Value.Value += node.Value.Value;

                    if (node.Next.Next != null)
                        node.Next.Next.Value.Value += node.Next.Value.Value;

                    node = Collapse(num, node, 0);
                    continue;
                }

                snail = num.FirstOrDefault(n => n.Value > 9);

                if (snail != null)
                {
                    var node = num.Find(snail);
                    num.AddBefore(node, new snail(node.Value.Value / 2, node.Value.Level + 1));
                    num.AddBefore(node, new snail(node.Value.Value - (node.Value.Value / 2), node.Value.Level + 1));
                    num.Remove(node);
                }
            } while (snail != null);

            return num;
        }

        LinkedList<snail> Add(LinkedList<snail> num, LinkedList<snail> next)
          => Reduce(new LinkedList<snail>(num.Union(next).Select(s => new snail(s.Value, s.Level + 1))));

        long Magnitude(LinkedList<snail> num)
        {
            while (num.Count > 1)
            {
                var level = num.Max(n => n.Level);
                for (var node = num.First; node != null && node.Next != null; node = node.Next)
                    if (node.Value.Level == node.Next.Value.Level && node.Value.Level == level)
                        node = Collapse(num, node, node.Value.Value * 3 + node.Next.Value.Value * 2);
            }
            return num.First.Value.Value;
        }

        var nums = input.Select(line => new LinkedList<snail>(Parse(line))).ToList();
        var sum = nums.Skip(1).Aggregate(nums.First(), (acc, next) => Add(acc, next));

        var sums = from a in nums
                   from b in nums
                   where a != b
                   select Magnitude(Add(a, b));

        return (Magnitude(sum), sums.Max());
    }
}