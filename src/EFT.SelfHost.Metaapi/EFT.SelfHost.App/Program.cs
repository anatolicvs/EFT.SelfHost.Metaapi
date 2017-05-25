using Topshelf;

namespace EFT.SelfHost.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public static void StartTopshelf()
        {
            HostFactory.Run(x =>
            {
                x.Service<WebServer>(s =>
                {
                    s.ConstructUsing(name => new WebServer());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("EFT.Clearpoint FX");
                x.SetDisplayName("Self Host Meta Trader Api.");
                x.SetServiceName("EFT.Selfhost.App");
            });
        }
    }
}
