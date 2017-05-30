namespace EFT.Meta.SelfHost.Api.Controller
{
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly AuthRepository authRepository = null;

        public RefreshTokensController(AuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        //[Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(authRepository.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await authRepository.RemoveRefreshToken(tokenId);

            if (result)
            {
                return Ok();
            }

            return BadRequest("Token Id does not exist");
        }
    }
}
