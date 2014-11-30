using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MassiveInteractiveGraph.Services
{
    [ServiceContract]
    public interface IGraphCalculations
    {
        [OperationContract]
        List<int> CalculateShortestRoute(int nodeId1, int nodeId2);
    }
}
