using Autofac;
using EFT.Infrastructure.Repositories.Dapper.Meta;
using EFT.Infrastructure.Repositories.Interfaces.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Meta.SelfService.DependencyResolvers.Autofac
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MetaTransactionRepository>().As<IMetaTransactionRepository>().SingleInstance();
        }
    }
}
