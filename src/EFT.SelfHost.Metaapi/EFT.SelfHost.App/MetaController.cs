using Autofac;
using EFT.Meta.Core.Model;
using EFT.Meta.SelfService;
using EFT.Meta.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace EFT.Meta.SelfHost.Api
{
    public class MetaController : ApiController
    {

        public async Task<IEnumerable<MetaAccount>> GetMetaAccountList()
        {
            using (var scope = MetaContainer.MetaContainer.Initialize().BeginLifetimeScope())
            {
                var metaService = scope.Resolve<IMetaService>();

                var metaAccounts = await Task.FromResult<List<MetaAccount>>(metaService.GetMetaAccountList("bora.ergul@live.com"));

                return metaAccounts;
            }
        }
    }
}
