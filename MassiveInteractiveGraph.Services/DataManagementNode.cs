using System.Runtime.Serialization;

namespace MassiveInteractiveGraph.Services
{
    [DataContract]
    public class DataManagementNode
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Label { get; set; }
    }
}