using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Models.Based_Entity
{
    public interface IBase
    {
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime InsertedAt { get; set; }
        DateTime UpdatedAt { get; set; }

    }
}
