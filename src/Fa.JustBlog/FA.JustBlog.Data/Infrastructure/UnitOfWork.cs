using FA.JustBlog.Data.Infrastructure.BaseRepository;
using FA.JustBlog.Models.Based_Entity;
using FA.JustBlog.Models.Common;
using System.Threading.Tasks;

namespace FA.JustBlog.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JustBlogDbContext _dbContext;

        public JustBlogDbContext DbContext => _dbContext;

        public UnitOfWork(JustBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private IGeneric<Category> _categoryRepository;

        public IGeneric<Category> CategoryRepository =>
            _categoryRepository ?? new Generic<Category>(_dbContext);

        private IGeneric<Tag> _tagRepository;

        public IGeneric<Tag> TagRepository =>
            _tagRepository ?? new Generic<Tag>(_dbContext);

        private IGeneric<Post> _postRepository;

        public IGeneric<Post> PostRepository =>
            _postRepository ?? new Generic<Post>(_dbContext);

        #region Methods
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();

        }
        public IGeneric<T> GenericRepository<T>() where T : Base
        {
            return new Generic<T>(_dbContext);
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
        #endregion
    }
}

