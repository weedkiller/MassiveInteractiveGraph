using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;

namespace MassiveInteractiveGraph.Services.ShortestPathCalculations
{
    public interface IShortestPathCalculator
    {
        void Init(IEnumerable<Tuple<int, int>> links);
        IEnumerable<int> CalculateShortestPath(int nodeId1, int nodeId2);
    }

    public class ShortestPathCalculator : IShortestPathCalculator
    {
        private IDictionary<int, List<int>> _adjacencyList;
        private IEnumerable<Tuple<int, int>> _links;

        public void Init(IEnumerable<Tuple<int, int>> links)
        {
            _links = links;

            _adjacencyList = new Dictionary<int, List<int>>();
            foreach (var link in links)
            {
                if (!_adjacencyList.ContainsKey(link.Item1))
                {
                    _adjacencyList.Add(link.Item1, new List<int>());
                }

                _adjacencyList[link.Item1].Add(link.Item2);
            }
        }

        public IEnumerable<int> CalculateShortestPath(int nodeId1, int nodeId2)
        {
            List<int> toReturn = null;

            var source = nodeId1;
            var target = nodeId2;

            var edges = _links.Select(l => new SEquatableEdge<int>(l.Item1, l.Item2));
            var graph = edges.ToAdjacencyGraph<int, SEquatableEdge<int>>(false);

            //IVertexAndEdgeListGraph<int, Edge<int>> graph = new AdjacencyGraph<int, Edge<int>>(false);
            
            Func<SEquatableEdge<int>, double> edgeCost = e => 1;

            TryFunc<int, IEnumerable<SEquatableEdge<int>>> tryGetPaths = graph.ShortestPathsDijkstra(edgeCost, source);

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