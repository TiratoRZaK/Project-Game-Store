using Ninject;
using PlaySpace.Abstract;
using PlaySpace.Infrastructure.Abstract;
using PlaySpace.Infrastructure.Concrete;
using PlaySpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaySpace.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IGameRepository>().To<EFGameRepository>();

            EmailSettings emailSettings = new EmailSettings();//Регистрация реализации обработчика заказов

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);//Регистрация реализации обработчика заказов
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}