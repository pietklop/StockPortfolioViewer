using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Facilities;
using log4net;

namespace Services.DI
{
    public class LogFacility : AbstractFacility
    {
        protected override void Init()
        {
            this.Kernel.Resolver.AddSubResolver((ISubDependencyResolver)new LogResolver());
        }
    }

    internal class LogResolver : ISubDependencyResolver
    {
        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return (object)dependency.TargetType == (object)typeof(ILog);
        }

        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return (object)LogManager.GetLogger(model.Implementation);
        }
    }

}