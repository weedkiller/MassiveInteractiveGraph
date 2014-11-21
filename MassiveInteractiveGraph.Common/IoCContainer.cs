using Ninject;

namespace MassiveInteractiveGraph.Common
{
    public class IoCContainer
    {
        private static StandardKernel _kernel = new StandardKernel();

        public static StandardKernel Kernel
        {
            get
            {
                return _kernel;
            }
        }

        public static void SetKernel(StandardKernel kernel)
        {
            _kernel = kernel;
        }
    }
}