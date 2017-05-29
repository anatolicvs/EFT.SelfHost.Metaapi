using Autofac;
using EFT.Infrastructure.Module;
using EFT.Infrastructure.Client;

namespace EFT.Infrastructure.Container
{
    public static class MetaSettingsContainer
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new SettingsModule("config.json"));
            builder.RegisterType<MetaClient>().As<IMetaClient>();
            return builder.Build();
        }
    }
}
