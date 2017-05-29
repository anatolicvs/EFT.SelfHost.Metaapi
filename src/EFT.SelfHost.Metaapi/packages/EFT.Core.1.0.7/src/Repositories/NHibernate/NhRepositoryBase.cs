using EFT.Core.Domains;
using EFT.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Repositories.NHibernate
{
    public abstract class NhRepositoryBase<TEntity> : IRepository<TEntity>
         where TEntity : class, IEntity, new()
    {
        private readonly NhHelper _nHibernateHelper;
        protected NhRepositoryBase(NhHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object whereConditions)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetList(object whereConditions = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetListAsync(object whereConditions = null)
        {
            throw new NotImplementedException();
        }
    
        public IServicePagedList<TEntity> GetPagedList(string conditions, string orderBy, int pageIndex = 0, int pageSize = 0, object parameters = null, bool includeItemCount = false)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
