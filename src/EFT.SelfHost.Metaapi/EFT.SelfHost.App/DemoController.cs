using System.Collections.Generic;
using System.Web.Http;

namespace EFT.Meta.SelfHost.Api
{
    public class DemoController : ApiController
    {
        // GET api/demo 
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }

      
    } 
}
