using log4net;
using MassiveInteractiveGraph.Common;
using MassiveInteractiveGraph.Dal;
using MassiveInteractiveGraph.Services.Dal;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MassiveInteractiveGraph.Services.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MassiveInteractiveGraph.Services.App_Start.NinjectWebCommon), "Stop")]

namespace MassiveInteractiveGraph.Services.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            IoCContainer.SetKernel(kernel);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILog>().ToMethod(ctx => LogManager.GetLogger("Services"));

            kernel.Bind<MassiveInteractiveGraphDbEntities>().ToSelf().InRequestScope().OnDeactivation(db => db.Dispose());
            kernel.Bind<IMassiveInteractiveGraphDbEntities>().ToMethod(context => context.Kernel.Get<MassiveInteractiveGraphDbEntities>());

            kernel.Bind<INodeDal>().To<NodeDal>();
            kernel.Bind<ILinkDal>().To<LinkDal>();
        }        
    }
}
