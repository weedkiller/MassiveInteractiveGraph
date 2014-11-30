using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MassiveInteractiveGraph.Services.Dal;

namespace MassiveInteractiveGraph.Services
{
    public class GraphCalculations : IGraphCalculations
    {
        private readonly INodeDal _nodeDal;
        private readonly ILinkDal _linkDal;

        public GraphCalculations(INodeDal nodeDal, ILinkDal linkDal)
        {
            _nodeDal = nodeDal;
            _linkDal = linkDal;
        }

        public List<int> CalculateShortestRoute(int nodeId1, int nodeId2)
        {
            var toReturn = new List<int>();

            //var nodes = _nodeDal.GetAll().ToList();
            var links = _linkDal.GetAll().ToList();

            //todo: implement

            return toReturn;
        }
    }
}
