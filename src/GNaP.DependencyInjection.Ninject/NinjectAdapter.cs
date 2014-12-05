namespace GNaP.DependencyInjection.Ninject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Ninject;
    
    public class NinjectAdapter : ContainerAdapter
    {
        private readonly IKernel _kernel;

        internal NinjectAdapter(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// When implemented by inheriting classes, this method will do the actual work of resolving
        /// the requested service instance.
        /// </summary>
        /// <param name="serviceType">Type of instance requested.</param>
        /// <param name="key">Name of registered service you want. May be null.</param>
        /// <returns>The requested service instance.</returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            try
            {
                return string.IsNullOrEmpty(key) 
                    ? _kernel.GetAll(serviceType).LastOrDefault() 
                    : _kernel.GetAll(serviceType, key).LastOrDefault();
            }
            catch (Exception e)
            {
                throw new ActivationException("Error during structuremap instance resolving", e);
            }
        }

        /// <summary>
        /// When implemented by inheriting classes, this method will do the actual work of
        /// resolving all the requested service instances.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>Sequence of service instance objects.</returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            try
            {
                return _kernel.GetAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        public override void Add(Type serviceType, Type implementationType)
        {
            _kernel.Bind(serviceType).To(implementationType);
        }

        public override void AddSingleton(Type serviceType, Type implementationType)
        {
            _kernel.Bind(serviceType).To(implementationType).InSingletonScope();
        }

        public override void AddInstance(Type serviceType, object implementation)
        {
            _kernel.Bind(serviceType).ToConstant(implementation);
        }
    }
}
