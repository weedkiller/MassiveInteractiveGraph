using System;
using System.Collections.Generic;
using System.Linq;
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
}
