using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using MassiveInteractiveGraph.Common;
using MassiveInteractiveGraph.WebClient.DataProviderService;
using MassiveInteractiveGraph.WebClient.Models;
using MassiveInteractiveGraph.WebClient.ViewModels;

namespace MassiveInteractiveGraph.WebClient.Controllers
{
    public class GraphController : Controller
    {
        private readonly IJsonSerializer _jsonSerializer;

        public GraphController(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public ActionResult Index()
        {
            var dataProvider = new DataProviderClient();

            var nodes = dataProvider.ListActiveNodes();
            var links = dataProvider.ListActiveLinks();

            var graph = new CytoscapeGraph()
            {
                Nodes = nodes.Select(n => new CytoscapeNode() { Id = n.Id, Label = n.Label }),
                Links = links.Select(l => new CytoscapeLink() { Id1 = l.Id1, Id2 = l.Id2 })
            };

            var vm = new GraphViewModel()
            {
                SerializedGraph = _jsonSerializer.Serialize(graph)
            };

            return View(vm);
        }

        public ActionResult FindPath(int sourceId, int targetId)
        {
            var graphCalculations = new GraphCalculationsService.GraphCalculationsClient();
            var path = graphCalculations.CalculateShortestRoute(sourceId, targetId);

            var result = _jsonSerializer.Serialize(path);
            return Content(result);
        }
    }
}