using Castle.Windsor;

namespace Services.DI
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
        public static T Resolve<T>() => Instance.Resolve<T>();

        public static void Dispose()
        {
            container?.Dispose();
            container = null;
        }
    }
}