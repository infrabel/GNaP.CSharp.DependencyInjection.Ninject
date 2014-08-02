namespace GNaP.DependencyInjection.Ninject
{
    using System;
    using global::Ninject;

    public static class Ninject
    {
        private static IKernel EmptyContainer
        {
            get { return new StandardKernel(); }
        }

        public static IContainerAdapter Create()
        {
            return new NinjectAdapter(EmptyContainer);
        }

        public static IContainerAdapter Create(Action<NinjectAdapter> configureContainerFunc)
        {
            if (configureContainerFunc == null)
                throw new ArgumentNullException("configureContainerFunc");

            var container = new NinjectAdapter(EmptyContainer);
            configureContainerFunc(container);
            return container;
        }

        public static IContainerAdapter Create(IKernel container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            return new NinjectAdapter(container);
        }
    }
}