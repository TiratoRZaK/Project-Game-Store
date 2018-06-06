using Moq;
using Ninject;
using Business_Logic_Layer.Abstract;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Data_Access_Layer.Repositories;

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
            Business_Logic_Layer.Models.EmailSettings emailSettings = new EmailSettings();//Регистрация реализации обработчика заказов
            kernel.Bind<IGameRepository>().To<DbGameRepository>();
            kernel.Bind<ICategoryRepository>().To<DbCategoryRepository>();
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);//Регистрация реализации обработчика заказов
        }
    }
}