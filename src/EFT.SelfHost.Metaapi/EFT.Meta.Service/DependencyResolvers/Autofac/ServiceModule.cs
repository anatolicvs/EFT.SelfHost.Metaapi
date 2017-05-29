using Autofac;
using EFT.Meta.Service;
using EFT.Meta.Service.Log;
using NLog;

namespace EFT.Meta.SelfService.DependencyResolvers.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MetaService>().As<IMetaService>().SingleInstance();
            builder.Register(context => new LogService(LogManager.GetCurrentClassLogger())).As<ILogService>().SingleInstance();
        }
    }
}
