using FA.JustBlog.Models.Based_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Data.Infrastructure.BaseRepository
{

    public class Generic<T> : IGeneric<T> where T : class, IBase
    {


        protected readonly JustBlogDbContext _context;
        protected readonly DbSet<T> DbSet;



        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T, TDbContext}"/> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public Generic(JustBlogDbContext context)
        {
            _context = context;
            var typeOfDbSet = typeof(DbSet<T>);
            foreach (var prop in context.GetType().GetProperties())
            {
                if (typeOfDbSet == prop.PropertyType)
                {
                    DbSet = prop.GetValue(context, null) as DbSet<T>;
                    break;
                }
            }

            if (DbSet == null)
            {
                DbSet = context.Set<T>();
            }


        }



        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<T> entities)
        {

            DbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


        public virtual void Delete(T entity, bool isHardDelete = false)
        {
            if (isHardDelete)
            {
                DbSet.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;

            }
        }


        public virtual void Delete(IEnumerable<T> entities, bool isHardDelete = false)
        {
            // Improve performance for hard delete
            if (isHardDelete)
                DbSet.RemoveRange(entities);
            else
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                }
        }

        public virtual void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false)
        {
            var entities = GetQuery(where).AsEnumerable();

            // Use this overload instead of using foreach to improve performance
            Delete(entities, isHardDelete);
        }

        public virtual T GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetByPageAsync(Expression<Func<T, bool>> condition, int size, int page)
        {
            return await DbSet.Where(condition).Skip(size * (page - 1)).Take(size).ToListAsync();
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in
                includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query) : query;
        }

        public virtual IQueryable<T> GetQuery()
        {
            return DbSet;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where);
        }


    }
}
