using Castle.Windsor;

namespace Dashboard.DI
{
    public static class CastleContainer
    {
        private static IWindsorContainer container;

        public static IWindsorContainer Instance
        {
            get { return container ??= new WindsorContainer(); }
            // exposing a setter alleviates some common component testing problems
            set => container = value;
        }

        // shortcut to make your life easier :)
        public static T Resolve<T>()
        {
            return Instance.Resolve<T>();
        }

        public static void Dispose()
        {
            container?.Dispose();
            container = null;
        }
    }
}