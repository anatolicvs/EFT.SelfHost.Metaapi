using P23.MetaTrader4.Manager;
using P23.MetaTrader4.Manager.Contracts;
using System;
namespace EFT.Infrastructure
{
    public static class MetaConnector
    {
        public static ClrWrapper Connect(string metaServer, int loginId, string password)
        {
            var connectionParameters = new ConnectionParameters() { Login = loginId, Password = password, Server = metaServer };
            var appt = AppDomain.CurrentDomain.BaseDirectory + @"mtmanapi\mtmanapi.dll";
            var connection = new ClrWrapper(connectionParameters, appt);
            return connection;
        }
    }
}
