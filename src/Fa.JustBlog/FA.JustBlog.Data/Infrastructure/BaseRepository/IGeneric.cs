using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Data.Infrastructure.BaseRepository
{
    public interface IGeneric<T>
    {
        T GetById(Guid id);

        Task<T> GetByIdAsync(Guid id);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity, bool isHardDelete = false);

        void Delete(IEnumerable<T> entities, bool isHardDelete = false);

        void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where, bool isHardDelete = false);

        IQueryable<T> GetQuery();
    }
}
