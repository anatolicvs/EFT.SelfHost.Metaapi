namespace EFT.Meta.SelfHost.Api.Controller
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly AuthRepository authRepository = null;

        public AccountController(AuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            var result = await authRepository.RegisterUser(userModel);

            var errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        //public IHttpActionResult Get() {

        //    var result =  new
        //    {
        //        IP = HttpContext.Current.Request.UserHostAddress,
        //        HostName =    HttpContext.Current.Request.UserHostName,
        //        Url = HttpContext.Current.Request.Url.Host,
        //        XOriginalURL = HttpContext.Current.Request.Headers.GetValues("X-Original-URL"),
        //        HeaderKeys = HttpContext.Current.Request.Headers.AllKeys,
        //        Origin = HttpContext.Current.Request.Headers.GetValues("Origin")
        //    };

        //    return Ok(result);
        //}

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
