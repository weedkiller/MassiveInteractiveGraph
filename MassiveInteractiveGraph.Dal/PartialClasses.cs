using System.ComponentModel.DataAnnotations;

namespace MassiveInteractiveGraph.Dal
{
    [MetadataType(typeof(NodeMetadata))]
    public partial class Node
    {
        public static void CopyAllValues(Node source, Node target)
        {
            target.Id = source.Id;
            target.Label = source.Label;

            target.AdjacentNodes.Clear(); //this has to be here to keep the ef foreign key references intact
            target.AdjacentNodes = source.AdjacentNodes;
        }
    }
}