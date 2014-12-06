using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MassiveInteractiveGraph.Services.ShortestPathCalculations
{
    public class MyDijkstraShortestPathCalculator:IShortestPathCalculator
    {
        private Dictionary<int, List<int>> _graph;

        public void Init(IEnumerable<Tuple<int, int>> links)
        {
            var edges1 = links.Select(l => new Tuple<int, int>(l.Item1, l.Item2));
            var edges2 = links.Select(l => new Tuple<int, int>(l.Item2, l.Item1));
            var edges = edges1.Union(edges2);

            _graph = new Dictionary<int, List<int>>();

            foreach (var edge in edges)
            {
                if (!_graph.ContainsKey(edge.Item1))
                {
                    _graph.Add(edge.Item1, new List<int>());
                }

                _graph[edge.Item1].Add(edge.Item2);
            }
        }

        public IEnumerable<int> CalculateShortestPath(int nodeId1, int nodeId2)
        {
            if (_graph == null) throw new Exception("Graph not initialized");
            if(!_graph.ContainsKey(nodeId1) || !_graph.ContainsKey(nodeId2)) return null;
            if (nodeId1 == nodeId2) return new[] {nodeId1};

            //init of the algorithm
            var currentNode = nodeId1;
            var visitedNodes = new List<int>();
            var notVisitedNodes = _graph.Keys.ToList();
            var calculatedDistances = new Dictionary<int, int>();
            var calculatedPaths = new Dictionary<int, int[]>();
            foreach (var key in _graph.Keys)
            {
                calculatedPaths.Add(key, new int[]{});
                if (key == nodeId1)
                {
                    calculatedDistances.Add(key, 0);
                    calculatedPaths[key] = new int[] {nodeId1};
                }
                else
                {
                    calculatedDistances.Add(key, int.MaxValue);
                }
            }

            //algorithm
            var distance = 1;
            while (notVisitedNodes.Any())
            {
                var neighbors = _graph[currentNode];
                var notVisitedNeighbors = neighbors.Where(n => notVisitedNodes.Contains(n));
                foreach (var notVisitedNeighbor in notVisitedNeighbors)
                {
                    if (distance < calculatedDistances[notVisitedNeighbor])
                    {
                        calculatedDistances[notVisitedNeighbor] = distance;
                        calculatedPaths[notVisitedNeighbor] = calculatedPaths[currentNode].Concat(new int[] { notVisitedNeighbor }).ToArray();
                    } 
                }

                if (currentNode == nodeId2)
                {
                    break;
                }

                visitedNodes.Add(currentNode);
                notVisitedNodes.Remove(currentNode);
                distance++;

                //pick next currentNode
                var minCalculatedNodeDistance = int.MaxValue;
                foreach (var notVisitedNode in notVisitedNodes)
                {
                    if (calculatedDistances[notVisitedNode] < minCalculatedNodeDistance)
                    {
                        minCalculatedNodeDistance = calculatedDistances[notVisitedNode];
                        currentNode = notVisitedNode;
                    }
                }

                if (minCalculatedNodeDistance == int.MaxValue)
                {
                    break;
                }

            }

            if (calculatedPaths.ContainsKey(nodeId2))
            {
                var shortestPath = calculatedPaths[nodeId2];
                return shortestPath;
            }
            else
            {
                return null;
            }
        }
    }
}