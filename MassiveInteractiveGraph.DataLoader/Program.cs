using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MassiveInteractiveGraph.DataLoader.DataManagementService;

namespace MassiveInteractiveGraph.DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DataManagementService.DataManagementClient();
            var activeNodes = service.ListActiveNodes();
            var activeLinks = service.ListActiveLinks();

            var importedNodes = new List<DataManagementService.DataManagementNode>();
            var importedLinks = new List<DataManagementService.DataManagementLink>();

            var dir = args[0];

            if (Directory.Exists(dir))
            {
                var files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(file);

                    var id = int.Parse(doc.SelectSingleNode("/node/id").InnerText);
                    var label = doc.SelectSingleNode("/node/label").InnerText;

                    importedNodes.Add(new DataManagementNode() {Id = id, Label = label});
                    
                    XmlNodeList adjacentIdNodes = doc.SelectNodes("/node/adjacentNodes/id");
                    foreach (XmlNode adjacentIdNode in adjacentIdNodes)
                    {
                        var adjacentId = int.Parse(adjacentIdNode.InnerText);
                        importedLinks.Add(new DataManagementLink() { Id1 = id, Id2 = adjacentId });
                    }
                }
            }

            var nodesToAdd = importedNodes.Where(n => !activeNodes.Exists(a => a.Id == n.Id)).ToList();
            var nodesToRename = importedNodes.Where(n => activeNodes.Exists(a => a.Id == n.Id) && activeNodes.Find(a => a.Id == n.Id).Label != n.Label).ToList();
            var nodesToDelete = activeNodes.Where(n => !importedNodes.Exists(a => a.Id == n.Id)).ToList();

            var linksToAdd = importedLinks.Where(l => !activeLinks.Exists(a => a.Id1 == l.Id1 && a.Id2 == l.Id2)).ToList();
            var linksToDelete = activeLinks.Where(l => !importedLinks.Exists(a => a.Id1 == l.Id1 && a.Id2 == l.Id2)).ToList();

            try
            {
                service.RenameNodes(nodesToRename);

                service.RemoveLinks(linksToDelete);
                service.RemoveNodes(nodesToDelete);

                service.AddNodes(nodesToAdd);
                service.AddLinks(linksToAdd);

                service.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                service.Abort();
            }
        }
    }
}
