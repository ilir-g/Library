using IG_CoreLibrary;
using IG_CoreLibrary.Repository;
using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace IG_WebApi.App_Start
{
    public class NinjectResolver : IDependencyResolver
    {
        protected static IKernel kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
           
        }

        public NinjectResolver(IKernel ninjectKernel, bool scope = false)
        {
            kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(kernel);
            }
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectResolver(AddRequestBindings(new ChildKernel(kernel)), true);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {

        }
    
        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
            kernel.Bind<IBaseRepository>().To<BaseRepository>().InSingletonScope();
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            kernel.Bind<IBaseRepository>().To<BaseRepository>().InSingletonScope();
            return kernel;
        }
    }
}