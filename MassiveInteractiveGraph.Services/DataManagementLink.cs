using System.Runtime.Serialization;

namespace MassiveInteractiveGraph.Services
{
    [DataContract]
    public class DataManagementLink
    {
        [DataMember]
        public int Id1 { get; set; }

        [DataMember]
        public int Id2 { get; set; }
    }
}