using System;
using Microsoft.Owin.Hosting;

namespace EFT.SelfHost.App
{
    public class WebServer
    {
        private IDisposable _webapp;

        public void Start()
        {
            _webapp = WebApp.Start<Startup>("http://localhost:9898");
        }

        public void Stop()
        {
            _webapp?.Dispose();
        }
    }
}
