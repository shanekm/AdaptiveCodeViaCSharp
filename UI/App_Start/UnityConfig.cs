using System;
using System.Data.Common;
using AdoNet;
using Contracts;
using Microsoft.Practices.Unity;

namespace UI.App_Start
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(
            () =>
                {
                    var container = new UnityContainer();
                    RegisterTypes(container);
                    return container;
                });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            container.RegisterType<IApplicationSettings, ConfigAppSettings>();
            container.RegisterType<IRoomRepository, AdoNetRoomRepository>();

            container.RegisterType<DbProviderFactory>(
                new InjectionFactory(
                    c =>
                    DbProviderFactories.GetFactory(c.Resolve<IApplicationSettings>().GetValue("DatabaseProviderName"))));
        }
    }
}