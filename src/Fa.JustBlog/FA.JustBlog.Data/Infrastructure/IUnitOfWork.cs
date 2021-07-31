using FA.JustBlog.Data.Infrastructure.BaseRepository;
using FA.JustBlog.Models.Based_Entity;
using FA.JustBlog.Models.Common;
using System;
using System.Threading.Tasks;

namespace FA.JustBlog.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        JustBlogDbContext DbContext { get; }

        IGeneric<Category> CategoryRepository { get; }

        IGeneric<Tag> TagRepository { get; }

        IGeneric<Post> PostRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IGeneric<T> GenericRepository<T>() where T : Base;
    }
}
