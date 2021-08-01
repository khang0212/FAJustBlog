using FA.JustBlog.Data.Infrastructure;
using FA.JustBlog.Models.Common;
using FA.JustBlog.Services.BaseService;

namespace FA.JustBlog.Services
{
    public class CategoryServices : BaseServices<Category>, ICategoryServices
    {
        public CategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
