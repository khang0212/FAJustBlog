using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<JustBlogDbContext>
    {
        protected override void Seed(JustBlogDbContext context)
        {

        }

    }
}
