using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MassiveInteractiveGraph.Services
{
    [ServiceContract]
    public interface IDataManagement
    {
        [OperationContract]
        List<DataManagementNode> ListActiveNodes();

        [OperationContract]
        void AddNodes(List<DataManagementNode> nodes);

        [OperationContract]
        void RemoveNodes(List<DataManagementNode> nodes);


        [OperationContract]
        List<DataManagementLink> ListActiveLinks();

        [OperationContract]
        void AddLinks(List<DataManagementLink> links);

        [OperationContract]
        void RemoveLinks(List<DataManagementLink> links);


        [OperationContract]
        void RenameNodes(List<DataManagementNode> nodes);
    }

    [DataContract]
    public class DataManagementLink
    {
        [DataMember]
        public int Id1 { get; set; }

        [DataMember]
        public int Id2 { get; set; }
    }

    [DataContract]
    public class DataManagementNode
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Label { get; set; }
    }
}
