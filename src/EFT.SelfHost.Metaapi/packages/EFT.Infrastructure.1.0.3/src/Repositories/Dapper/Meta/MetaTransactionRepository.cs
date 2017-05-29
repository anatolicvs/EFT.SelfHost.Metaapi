using EFT.Core.Repositories.Dapper;
using EFT.Domain.Entities.Meta;
using EFT.Infrastructure.Repositories.Interfaces.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Infrastructure.Repositories.Dapper.Meta
{
    public class MetaTransactionRepository : DapperRepositoryBase<MetaTransaction>, IMetaTransactionRepository
    {
        public MetaTransactionRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
    }
}
