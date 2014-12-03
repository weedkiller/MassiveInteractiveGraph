using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MassiveInteractiveGraph.Services.Dal;
using MassiveInteractiveGraph.Services.ShortestPathCalculations;

namespace MassiveInteractiveGraph.Services
{
    public class GraphCalculations : IGraphCalculations
    {
        private readonly ILinkDal _linkDal;

        private readonly IShortestPathCalculator _calculator;

        public GraphCalculations(ILinkDal linkDal, IShortestPathCalculator calculator)
        {
            _linkDal = linkDal;
            _calculator = calculator;
        }

        public List<int> CalculateShortestRoute(int nodeId1, int nodeId2)
        {
            var toReturn = new List<int>();
            var links = _linkDal.GetAll().ToList();

            _calculator.Init(links.Select(l => new Tuple<int, int>(l.Node1Id, l.Node2Id)));
            var path = _calculator.CalculateShortestPath(nodeId1, nodeId2);
            if (path != null)
            {
                toReturn = path.ToList();
            }

            return toReturn;
        }
    }
}
