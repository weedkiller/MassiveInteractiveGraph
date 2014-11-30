using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MassiveInteractiveGraph.Dal;
using MassiveInteractiveGraph.Services.Dal;

namespace MassiveInteractiveGraph.Services
{
    public class DataManagement : IDataManagement
    {
        private readonly INodeDal _nodeDal;
        private readonly ILinkDal _linkDal;

        public DataManagement(INodeDal nodeDal, ILinkDal linkDal)
        {
            _nodeDal = nodeDal;
            _linkDal = linkDal;
        }

        public List<DataManagementNode> ListActiveNodes()
        {
            var nodes = _nodeDal.GetAll().Select(n => new DataManagementNode() {Id = n.Id, Label = n.Label}).ToList();
            return nodes;
        }

        public void AddNodes(List<DataManagementNode> nodes)
        {
            foreach (var node in nodes)
            {
                _nodeDal.AddOrEdit(new Node() {Id = node.Id, Label = node.Label});
            }

            _nodeDal.Save();
        }

        public void RemoveNodes(List<DataManagementNode> nodes)
        {
            foreach (var node in nodes)
            {
                _nodeDal.Delete(node.Id);
            }
            _nodeDal.Save();
        }

        public List<DataManagementLink> ListActiveLinks()
        {
            var links = _linkDal.GetAll().Select(l => new DataManagementLink() { Id1 = l.Node1Id, Id2 = l.Node2Id }).ToList();
            return links;
        }

        public void AddLinks(List<DataManagementLink> links)
        {
            var linksFromNodes = new Dictionary<int, List<int>>();
            foreach (var link in links)
            {
                if (!linksFromNodes.ContainsKey(link.Id1))
                {
                    linksFromNodes.Add(link.Id1, new List<int>());
                }

                linksFromNodes[link.Id1].Add(link.Id2);
            }

            foreach (var sourceId in linksFromNodes.Keys)
            {
                foreach (var linkedId in linksFromNodes[sourceId])
                {
                    _linkDal.Add(new Link() {Node1Id = sourceId, Node2Id = linkedId});
                }
            }

            _linkDal.Save();
        }

        public void RemoveLinks(List<DataManagementLink> links)
        {
            foreach (var link in links)
            {
                _linkDal.Delete(link.Id1, link.Id2);
            }

            _linkDal.Save();
        }

        public void RenameNodes(List<DataManagementNode> nodes)
        {
            foreach (var node in nodes)
            {
                var n = _nodeDal.Find(node.Id);
                n.Label = node.Label;

                _nodeDal.AddOrEdit(n);
            }

            _nodeDal.Save();
        }
    }
}
