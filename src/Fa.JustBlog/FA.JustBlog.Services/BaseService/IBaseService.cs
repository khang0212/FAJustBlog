using Fa.JustBlog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Services.BaseService
{
    public interface IBaseService<TEntity>
    {
        int Add(TEntity entity);

        Task<int> AddAsync(TEntity entity);

        int AddRange(IEnumerable<TEntity> entities);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        bool Delete(Guid id);

        Task<bool> DeleteAsync(Guid id);

        TEntity GetById(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();
        
     
        Task<Paginated<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10);
    }
}
