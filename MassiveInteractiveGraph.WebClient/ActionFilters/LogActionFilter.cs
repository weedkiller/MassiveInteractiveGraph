using System;
using System.Collections.Generic;
using System.Web.Mvc;
using log4net;
using MassiveInteractiveGraph.Common;
using Ninject;

namespace MassiveInteractiveGraph.WebClient.ActionFilters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private bool _execute = true; // !!! true by default
        public bool Execute
        {
            get { return _execute; }
            set { _execute = value; }
        }

        readonly ILog _log;
        readonly IJsonSerializer _jsonSerializer;

        public LogActionFilterAttribute()
        {
            _log = IoCContainer.Kernel.Get<ILog>();
            _jsonSerializer = IoCContainer.Kernel.Get<IJsonSerializer>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.ActionDescriptor.ControllerDescriptor, filterContext.ActionDescriptor, filterContext.ActionParameters);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        private void Log(string methodName, ControllerDescriptor controllerDescriptor, ActionDescriptor actionDescriptor, IDictionary<string, object> actionParameters)
        {
            if (Execute)
            {
                var actionParametersToLog = new Dictionary<string, object>();

                foreach (var parameter in actionDescriptor.GetParameters())
                {
                    var attr = parameter.GetCustomAttributes(typeof(NoLogAttribute), false);
                    if (attr.Length == 0)
                    {
                        actionParametersToLog.Add(parameter.ParameterName, actionParameters[parameter.ParameterName]);
                    }
                }

                string parameters = string.Empty;
                if (actionParametersToLog.Count > 0)
                {
                    parameters = _jsonSerializer.Serialize(actionParametersToLog);
                }

                var message = String.Format("{0} controller:{1} action: {2} params:{3}", methodName, controllerDescriptor.ControllerName, actionDescriptor.ActionName, parameters);
                _log.Debug(message);
            }
        }
    }
}
