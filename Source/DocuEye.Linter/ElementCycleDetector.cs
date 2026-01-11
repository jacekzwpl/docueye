using DocuEye.Linter.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Linter
{

    public static class ElementCycleDetector
    {
        public static IReadOnlyList<string>? CurrentCycle = null;
        public static bool CycleExists(string elementType, IEnumerable<LinterModelRelationship> relationships)
        {
            CurrentCycle = FindCycle(elementType, relationships);
            return CurrentCycle != null;
        }


        private static IReadOnlyList<string>? FindCycle(string elementType, IEnumerable<LinterModelRelationship> relationships)
        {
            var graph = relationships.Where(r => r.Source.Tags.Contains(elementType) && r.Destination.Tags.Contains(elementType))
            .GroupBy(d => d.Source.Identifier)
            .ToDictionary(
                g => g.Key,
                g => g.Select(d => d.Destination.Identifier).ToList()
            );

            var visited = new HashSet<string>();
            var stack = new Stack<string>();
            var inStack = new HashSet<string>();

            foreach (var node in graph.Keys)
            {
                if (!visited.Contains(node))
                {
                    var cycle = DepthFirstSearch(node, graph, visited, stack, inStack);
                    if (cycle != null)
                        return cycle;
                }
            }

            return null;
        }

        private static IReadOnlyList<string>? DepthFirstSearch(
            string node,
            Dictionary<string, List<string>> graph,
            HashSet<string> visited,
            Stack<string> stack,
            HashSet<string> inStack)
        {
            visited.Add(node);
            stack.Push(node);
            inStack.Add(node);

            if (graph.TryGetValue(node, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        var cycle = DepthFirstSearch(neighbor, graph, visited, stack, inStack);
                        if (cycle != null)
                            return cycle;
                    }
                    else if (inStack.Contains(neighbor))
                    {
                        // Odtworzenie cyklu
                        return BuildCyclePath(stack, neighbor);
                    }
                }
            }

            stack.Pop();
            inStack.Remove(node);
            return null;
        }

        private static IReadOnlyList<string> BuildCyclePath(
        Stack<string> stack,
        string cycleStart)
        {
            var path = new List<string>();
            foreach (var node in stack)
            {
                path.Add(node);
                if (node == cycleStart)
                    break;
            }

            path.Reverse();
            path.Add(cycleStart); // domknięcie cyklu

            return path;
        }
    }
}
