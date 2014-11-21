using System;

namespace MassiveInteractiveGraph.WebClient.ActionFilters
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NoLogAttribute : Attribute 
    {
    }
}