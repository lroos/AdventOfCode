public class Day12
{   
    public (int, int) Run(string[] input)
    {
        var edges = input.Select(s => s.Split('-'));
        var graph = edges.Union(edges.Select(e => e.Reverse().ToArray()))
            .ToLookup(e => e[0], e => e[1]);

        string[][] GetPaths(string vertex, string[] visited, int max)
        {
            var path = visited.Append(vertex).ToArray();
            if (vertex == "end")
                return new string[][] { path };

            var lower = path.Where(v => char.IsLower(v[0])).ToArray();
            var exclude = lower.GroupBy(v => v).Any(g => g.Count() == max) ? lower 
                : new string[] { "start" };
            return graph[vertex].Except(exclude)
                .SelectMany(node => GetPaths(node, path, max))
                .ToArray();            
        }

        int paths(int max) => GetPaths("start", new string[0], max).Count();

        return (paths(1), paths(2));
    }
}