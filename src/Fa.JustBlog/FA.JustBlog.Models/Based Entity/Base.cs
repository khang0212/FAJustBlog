using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Models.Based_Entity
{
    public class Base : IBase
    {

        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
