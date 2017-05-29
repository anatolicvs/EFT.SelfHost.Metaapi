using EFT.Infrastructure.Settings;
using P23.MetaTrader4.Manager;


namespace EFT.Infrastructure.Client
{
    public class MetaClient : IMetaClient
    {
        private readonly MetaSettings _metaSettings;
        public MetaClient(MetaSettings metaSettings)
        {
            _metaSettings = metaSettings;
            Client = MetaConnector.Connect(_metaSettings.MetaServer, _metaSettings.LoginId, _metaSettings.Password);
            MetaSettings = _metaSettings;
        }

        public ClrWrapper Client { get; set; }
        public MetaSettings MetaSettings { get; set; }
        public void Close()
        {
            Client.Dispose();
        }
    }
}
