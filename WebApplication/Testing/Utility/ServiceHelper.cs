using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;
using com.Sconit.Service.MasterData;
using WatiN.Core;

namespace SconitTesting.Utility
{
    public static class ServiceHelper
    {
        private static IWindsorContainer _container;

        /// <summary>
        /// Castle Windsor Container
        /// </summary>
        public static IWindsorContainer container
        {
            get
            {
                if (_container == null)
                {
                    _container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
                }
                return _container;
            }
            set { _container = value; }
        }

        public static T GetService<T>(string serviceName)
        {
            return container.Resolve<T>(serviceName);
        }
    }
}
