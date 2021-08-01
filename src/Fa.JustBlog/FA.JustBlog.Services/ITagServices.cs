using FA.JustBlog.Models.Common;
using FA.JustBlog.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Services
{
    public interface ITagServices : IBaseService<Tag>
    {

        Task<int> CountTagsForPostAsync(string post);

        Task<IEnumerable<Tag>> GetTagsByPostAsync(string post);

        IEnumerable<Tag> GetHighestViewCountTag(int count);
    }
}
