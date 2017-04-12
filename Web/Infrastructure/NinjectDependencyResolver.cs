using Domain.Abstract;
using Domain.Context;
using Domain.Organization;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Infrastructure
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
            kernel.Bind<IAccessory>().To<AccessoriesRepository>();

            EmailSettings email = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrder>().To<EmailOrder>().WithConstructorArgument("settings", email);
            //Mock<IAccessory> mock = new Mock<IAccessory>();
            //mock.Setup(m => m.Accessories).Returns(new List<Accessory>
            //{
            //    new Accessory { Name = "Trinket", Cost = 1499 },
            //    new Accessory { Name = "Chain", Cost=2299 },
            //    new Accessory { Name = "Brangle", Cost=899.4M }
            //});
            //kernel.Bind<IAccessory>().ToConstant(mock.Object);
        }
    }
}