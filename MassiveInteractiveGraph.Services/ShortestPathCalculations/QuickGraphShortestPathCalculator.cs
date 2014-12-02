using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;

namespace MassiveInteractiveGraph.Services.ShortestPathCalculations
{
    public class QuickGraphShortestPathCalculator : IShortestPathCalculator
    {
        private AdjacencyGraph<int, SEquatableEdge<int>> _graph;

        public void Init(IEnumerable<Tuple<int, int>> links)
        {
            var edges1 = links.Select(l => new SEquatableEdge<int>(l.Item1, l.Item2));
            var edges2 = links.Select(l => new SEquatableEdge<int>(l.Item2, l.Item1));
            var edges = edges1.Union(edges2);
            _graph = edges.ToAdjacencyGraph<int, SEquatableEdge<int>>(false);
        }

        public IEnumerable<int> CalculateShortestPath(int nodeId1, int nodeId2)
        {
            if (_graph == null) throw new Exception("Graph not initialized");
            if (nodeId1 == nodeId2) return new[] {nodeId1};

            List<int> toReturn = null;

            var source = nodeId1;
            var target = nodeId2;

            //IVertexAndEdgeListGraph<int, Edge<int>> graph = new AdjacencyGraph<int, Edge<int>>(false);
            
            Func<SEquatableEdge<int>, double> edgeCost = e => 1;

            TryFunc<int, IEnumerable<SEquatableEdge<int>>> tryGetPaths = _graph.ShortestPathsDijkstra(edgeCost, source);

            IEnumerable<SEquatableEdge<int>> path;
            var pathFound = tryGetPaths(target, out path);

            if (pathFound)
            {
                var vertices = path.Select(p => new Tuple<int, int>(p.Source, p.Target)).ToList();
                
                toReturn = vertices.Select(v => v.Item1).ToList();
                toReturn.Add(vertices.Last().Item2);
            }

            return toReturn;
        }
    }

}