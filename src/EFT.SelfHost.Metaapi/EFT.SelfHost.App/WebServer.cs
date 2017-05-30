using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace EFT.Meta.SelfHost.Api
{
    public class WebServer
    {
        private IDisposable _webapp;

        public void Start()
        {
            _webapp = WebApp.Start<Startup>("http://35.166.137.107:8686");
        }

        public void Stop()
        {
            _webapp?.Dispose();
        }
    }
}
