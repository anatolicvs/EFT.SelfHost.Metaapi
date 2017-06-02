using EFT.Core.Services.Interfaces;
using EFT.Meta.Core.Model;
using System.Collections.Generic;

namespace EFT.Meta.SelfService
{
    public interface IMetaService :  IService
    {
        ReturnStatus UserCheckPassword(int loginId, string password);
        ReturnStatus UserPasswordSet(int loginId, string password);
        List<MetaAccount> GetMetaAccountList(string Username);
        MetaPersonalDetail GetMetaPersonalDetail(int loginId);
        ReturnStatus EFTTransaction(MetaTransaction trans);
        ReturnStatus AddDeposit(MetaDepositModel model); 
    }
}
