using EFT.Infrastructure.Settings;
using P23.MetaTrader4.Manager;

namespace EFT.Infrastructure.Client
{
    public interface IMetaClient
    {
        ClrWrapper Client { get; set; }
        MetaSettings MetaSettings { get; set; }
        void Close();
    }
}
