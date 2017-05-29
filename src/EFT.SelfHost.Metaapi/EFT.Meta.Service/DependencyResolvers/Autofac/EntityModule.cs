using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EFT.Infrastructure.Repositories.Dapper.Helper;
using EFT.Core.Helper;
using EFT.Core.Repositories.Dapper;
namespace EFT.Meta.SelfService.DependencyResolvers.Autofac
{
    public class EntityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlDapperHelper>().As<DapperHelper>().As<IUnitofwork>().SingleInstance();
        }
    }
}
