using EFT.Core.Domains;
using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Get(object id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object whereConditions);
        IEnumerable<TEntity> GetList(object whereConditions = null);
        IServicePagedList<TEntity> GetPagedList(string conditions, string orderBy, int pageIndex = 0, int pageSize = 0, object parameters = null, bool includeItemCount = false);
        Task<TEntity> GetAsync(object id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetListAsync(object whereConditions = null);
    }
}
