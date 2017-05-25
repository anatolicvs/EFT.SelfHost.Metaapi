using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace EFT.SelfHost.App
{
    public class CustomMiddleware : OwinMiddleware
    {
        public CustomMiddleware(OwinMiddleware next) : base(next)
        {
        }
        public async override Task Invoke(IOwinContext context)
        {
            context.Response.Headers["MachineName"] = Environment.MachineName;

            await Next.Invoke(context);
        }
    }
}
