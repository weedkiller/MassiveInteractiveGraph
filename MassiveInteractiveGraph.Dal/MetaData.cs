using System.ComponentModel.DataAnnotations;

namespace MassiveInteractiveGraph.Dal
{
    public class NodeMetadata
    {
        [Required(ErrorMessage = "Label is required")]
        public string Label { get; set; }
    }
}