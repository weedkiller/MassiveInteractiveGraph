using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MassiveInteractiveGraph.Services.Dal;

namespace MassiveInteractiveGraph.Services
{
    public class DataProvider : IDataProvider
    {
        private readonly INodeDal _nodeDal;
        private readonly ILinkDal _linkDal;

        public DataProvider(INodeDal nodeDal, ILinkDal linkDal)
        {
            _nodeDal = nodeDal;
            _linkDal = linkDal;
        }

        public List<DataManagementNode> ListActiveNodes()
        {
            var nodes = _nodeDal.GetAll().Select(n => new DataManagementNode() { Id = n.Id, Label = n.Label }).ToList();
            return nodes;
        }

        public List<DataManagementLink> ListActiveLinks()
        {
            var links = _linkDal.GetAll().Select(l => new DataManagementLink() { Id1 = l.Node1Id, Id2 = l.Node2Id }).ToList();
            return links;
        }
    }
}
