using Autofac;

namespace EFT.Meta.SelfHost.Api.MetaContainer
{
    public static class MetaContainer
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<SelfService.DependencyResolvers.Autofac.EntityModule>();
            builder.RegisterModule<SelfService.DependencyResolvers.Autofac.RepositoryModule>();
            builder.RegisterModule<SelfService.DependencyResolvers.Autofac.ServiceModule>();
            return builder.Build();
        }
    }
}
