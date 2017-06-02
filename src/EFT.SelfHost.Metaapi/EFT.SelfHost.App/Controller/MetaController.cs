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
using System.Web;
using EFT.Meta.SelfHost.Api.Models;

namespace EFT.Meta.SelfHost.Api
{
    [RoutePrefix("api/Meta")]
    public class MetaController : ApiController
    {
        [Authorize]
        [Route("GetMetaAccountList")]
        [HttpPost]
        public async Task<IHttpActionResult> GetMetaAccountList(MetaAccountModel model)
        {
            using (var scope = MetaContainer.MetaContainer.Initialize().BeginLifetimeScope())
            {
                var metaService = scope.Resolve<IMetaService>();

                var metaAccounts = await Task.FromResult<List<MetaAccount>>(metaService.GetMetaAccountList(model.Email));

                return Json(metaAccounts);
            }
        }

        [Authorize]
        [Route("MoneyTransaction")]
        [HttpPost]
        public async Task<IHttpActionResult> MoneyTransaction(MetaTransaction model)
        {
            using (var scope = MetaContainer.MetaContainer.Initialize().BeginLifetimeScope())
            {   
                var metaService = scope.Resolve<IMetaService>();

                var result = await Task.FromResult<ReturnStatus>(metaService.EFTTransaction(model));

                return Json(result);
            }
        }

        [Authorize]
        [Route("GetMetaPersonalDetail")]
        [HttpPost]
        public async Task<IHttpActionResult> GetMetaPersonalDetail(MetaPersonalDetailModel model)
        {
            using (var scope = MetaContainer.MetaContainer.Initialize().BeginLifetimeScope())
            {
                var metaService = scope.Resolve<IMetaService>();


                var result = await Task.FromResult<MetaPersonalDetail>(metaService.GetMetaPersonalDetail(int.Parse(model.AccountNo)));

                return Json(result);
            }
        }


    }
}
