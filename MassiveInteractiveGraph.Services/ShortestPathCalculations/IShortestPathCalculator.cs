using System;
using System.Collections.Generic;

namespace MassiveInteractiveGraph.Services.ShortestPathCalculations
{
    public interface IShortestPathCalculator
    {
        void Init(IEnumerable<Tuple<int, int>> links);
        IEnumerable<int> CalculateShortestPath(int nodeId1, int nodeId2);
    }
}