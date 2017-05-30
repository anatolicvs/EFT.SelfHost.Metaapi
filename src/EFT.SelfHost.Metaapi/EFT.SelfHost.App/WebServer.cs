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
            _webapp = WebApp.Start<Startup>(Properties.Settings.Default.HostPort);
        }

        public void Stop()
        {
            _webapp?.Dispose();
        }
    }
}
