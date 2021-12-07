using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Util
{
    public static IEnumerable<int> range(int a, int b) => (a == b)
        ? Enumerable.Repeat(a, int.MaxValue)
        : (a < b) ? Enumerable.Range(a, Math.Abs(a - b) + 1) : Enumerable.Range(b, Math.Abs(a - b) + 1).Reverse();
}