using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Meta.Core.Model;
using Autofac;
using System.Transactions;
using P23.MetaTrader4.Manager.Contracts;
using EFT.Infrastructure.Container;
using EFT.Infrastructure.Client;
using EFT.Meta.Service.Log;
using EFT.Infrastructure.Repositories.Interfaces.Meta;
using EFT.Meta.Service;

namespace EFT.Meta.SelfService
{
    public class MetaService : IMetaService
    {

        private readonly static IContainer _metaContainer = MetaSettingsContainer.Initialize();
        private readonly ILogService _logger;
        private readonly IMetaTransactionRepository _metaTransactionRepository;
        public MetaService(IMetaTransactionRepository metaTransactionRepository)
        {
            _metaTransactionRepository = metaTransactionRepository;
            _logger = new LogService(NLog.LogManager.GetCurrentClassLogger());
        }

        public ReturnStatus AddDeposit(MetaDepositModel model)
        {
            try
            {
                var status = new ReturnStatus();
                using (var scope = _metaContainer.BeginLifetimeScope())
                {
                    var meta = scope.Resolve<IMetaClient>();

                    using (var transactionScope = new TransactionScope())
                    {
                        TradeTransInfo tinfo = new TradeTransInfo();
                        tinfo.Type = TradeTransactionType.BrBalance;
                        tinfo.Cmd = TradeCommand.Balance;
                        tinfo.OrderBy = int.Parse(model.ToAccount);
                        tinfo.Price = model.Amount;
                        tinfo.Comment = $"To {model.ToAccount} / {tinfo.Price} by Web";
                        _logger.Info(tinfo.Comment);

                        status.Code = meta.Client.TradeTransaction(tinfo);
                        status.Status = meta.Client.ErrorDescription(status.Code);
                        transactionScope.Complete();
                        return status;
                    }
                }
            }
            catch (Exception e)
            {

                _logger.Error(e);
                return null;
            }
        }

        /// <summary>
        /// (+) or (-) Deposit or Witdraw Method.
        /// </summary>
        /// <returns>MetaTransaction</returns>
        public ReturnStatus EFTTransaction(MetaTransaction trans)
        {
            try
            {
                var status = new ReturnStatus();
                using (var scope = _metaContainer.BeginLifetimeScope())
                {
                    var meta = scope.Resolve<IMetaClient>();

                    if (trans.FromAccount != trans.ToAccount)
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            TradeTransInfo info = new TradeTransInfo();
                            MarginLevel mLevel = meta.Client.MarginLevelRequest(trans.FromAccount);
                            if (mLevel.MarginFree > trans.Amount)
                            {
                                //--- create transaction
                                info.Type = TradeTransactionType.BrBalance;
                                info.Cmd = TradeCommand.Balance;
                                info.OrderBy = trans.FromAccount;
                                info.Price = (-1 * trans.Amount);
                                info.Comment = $"To {trans.ToAccount} / {info.Price} by Web";
                                _logger.Info(info.Comment);
                                //--- do transaction
                                int resFrom = meta.Client.TradeTransaction(info);

                                if (resFrom == 0)
                                {
                                    //--- create transaction
                                    info.Type = TradeTransactionType.BrBalance;
                                    info.Cmd = TradeCommand.Balance;
                                    info.OrderBy = trans.ToAccount;
                                    info.Price = trans.Amount;
                                    info.Comment = $"From {trans.FromAccount} / {info.Price} by Web";
                                    _logger.Info(info.Comment);

                                    //--- do transaction
                                    int resTo = meta.Client.TradeTransaction(info);
                                    if (resTo == 0)
                                    {
                                        status.Code = 0;
                                        status.Status = "Transaction Successful.";
                                        ts.Complete();

                                        var mTrans = new Domain.Entities.Meta.MetaTransaction()
                                        {
                                            FromAccount = trans.FromAccount,
                                            ToAccount = trans.ToAccount,
                                            Amount = trans.Amount,
                                            CreateDate = DateTime.Now,
                                            LastIP = trans.LastIP,
                                            UserId = trans.UserId
                                        };
                                        _metaTransactionRepository.Add(mTrans);
                                    }
                                    else
                                    {
                                        status.Code = resTo;
                                        status.Status = "Transaction ToAccount failed!Running Rollback";
                                    }
                                }
                                else
                                {
                                    status.Code = resFrom;
                                    status.Status = "Transaction FromAccount failed!Running Rollback";
                                }

                            }
                            else
                            {
                                status.Code = -1;
                                status.Status = $"From {trans.FromAccount} Free Margin not enough!";
                            }
                        }
                    }
                    else
                    {
                        status.Code = -1;
                        status.Status = "FromAccount should not equal ToAccount";
                    }
                    return status;
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex);
                return null;
            }
        }
        public List<MetaAccount> GetMetaAccountList(string username)
        {
            try
            {
                var mAccounts = new List<MetaAccount>();
                using (var scope = _metaContainer.BeginLifetimeScope())
                {
                    var meta = scope.Resolve<IMetaClient>();
                    var users = meta.Client.UsersRequest().ToList()
                                .Where(c => c.Email.Equals(username))
                                .ToList();
                    if (users.Count > 0)
                    {
                        foreach (var user in users)
                        {
                            MetaAccount account = new MetaAccount();
                            account.AccountNo = user.Login;
                            account.AccountState = user.Enable == 1 ? "Active" : "Pasive";
                            account.AccountType = user.Status;
                            account.Balance = user.Balance;
                            account.Currency = "USD";
                            account.Leverage = user.Leverage;
                            account.isDemo = meta.MetaSettings.MetaServer == "185.96.244.37:443" ? true : false;
                            MarginLevel mLevel = meta.Client.MarginLevelRequest(user.Login);
                            account.MarginLevel = mLevel.Margin;
                            account.FreeMargin = mLevel.MarginFree;
                            mAccounts.Add(account);
                        }
                    }

                    return mAccounts;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// Get Personal Detail on MetaTrader4 Server.
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns>MetaPersonDetail</returns>
        public MetaPersonalDetail GetMetaPersonalDetail(int loginId)
        {
            var mpDetail = new MetaPersonalDetail();
            try
            {
                using (var scope = _metaContainer.BeginLifetimeScope())
                {
                    var meta = scope.Resolve<IMetaClient>();
                    var user = meta.Client.UsersRequest().ToList()
                               .Where(u => u.Login.Equals(loginId))
                               .FirstOrDefault();
                    if (user != null)
                    {
                        mpDetail.Address = user.Address;
                        mpDetail.Balance = user.Balance;
                        mpDetail.City = user.City;
                        mpDetail.Country = user.Country;
                        mpDetail.Credit = user.Credit;
                        mpDetail.Email = user.Email;
                        mpDetail.Group = user.Group;
                        mpDetail.Id = user.Id;
                        mpDetail.InterestRate = user.InterestRate;
                        mpDetail.LastDate = new DateTime(1970, 1, 1).AddSeconds(user.LastDate);
                        mpDetail.LastIp = user.LastIp;
                        mpDetail.Login = user.Login;
                        mpDetail.Name = user.Name;
                        mpDetail.Phone = user.Phone;
                        mpDetail.PrevBalance = user.PrevBalance;
                        mpDetail.PrevEquity = user.PrevEquity;
                        mpDetail.PrevMonthBalance = user.PrevMonthBalance;
                        mpDetail.PrevMonthEquity = user.PrevMonthEquity;
                        mpDetail.Regdate = new DateTime(1970, 1, 1).AddSeconds(user.Regdate);
                        mpDetail.State = user.State;
                        mpDetail.Status = user.Status;
                        mpDetail.ZipCode = user.ZipCode;
                    }
                }

                return mpDetail;
            }
            catch (Exception ex)
            {

                _logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// return Code "0" is OK.
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public ReturnStatus UserCheckPassword(int loginId, string password)
        {
            var status = new ReturnStatus();
            try
            {
                using (var scope = _metaContainer.BeginLifetimeScope())
                {
                    var meta = scope.Resolve<IMetaClient>();
                    status.Code = meta.Client.UserPasswordCheck(loginId, password);
                    status.Status = meta.Client.ErrorDescription(status.Code);
                }
                return status;
            }
            catch (Exception ex)
            {

                _logger.Error(ex);
                return null;
            }
        }
        /// <summary>
        /// return Code "0" is OK.
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ReturnStatus UserPasswordSet(int loginId, string password)
        {
            var status = new ReturnStatus();
            try
            {
                using (var scope = _metaContainer.BeginLifetimeScope())
                {
                    var meta = scope.Resolve<IMetaClient>();
                    status.Code = meta.Client.UserPasswordSet(loginId, password, 0, 0);
                    status.Status = meta.Client.ErrorDescription(status.Code);
                }
                return status;
            }
            catch (Exception ex)
            {

                _logger.Error(ex);
                return null;
            }
        }
    }
}
