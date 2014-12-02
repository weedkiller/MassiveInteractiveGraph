using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MassiveInteractiveGraph.WebClient.Models
{
    public class CytoscapeGraph
    {
        public IEnumerable<CytoscapeNode> Nodes { get; set; }
        public IEnumerable<CytoscapeLink> Links { get; set; }
    }

    public class CytoscapeLink
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }
    }

    public class CytoscapeNode
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
}