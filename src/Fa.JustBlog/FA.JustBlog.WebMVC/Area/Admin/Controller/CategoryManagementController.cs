using FA.JustBlog.Data;
using FA.JustBlog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Area.Admin.Controller
{
    public class CategoryManagementController : Controller
    {
        private JustBlogDbContext db = new JustBlogDbContext();
        private readonly ICategoryServices _categoryServices;

        public CategoryManagementController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }



        // GET: Admin/CategoryManagement
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 2)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["UrlSlugSortParm"] = sortOrder == "UrlSlug" ? "urlSlug_desc" : "UrlSlug";
            ViewData["TotalPostsSortParm"] = sortOrder == "TotalPosts" ? "totalPosts_desc" : "TotalPosts";
            ViewData["InsertedAtSortParm"] = sortOrder == "InsertedAt" ? "insertedAt_desc" : "InsertedAt";
            ViewData["UpdatedAtSortParm"] = sortOrder == "UpdatedAt" ? "updatedAt_desc" : "UpdatedAt";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            // x => x.Name.Contains(searchString)
            Expression<Func<Category, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }

            // q => q.OrderByDescending(c => c.Name)
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = q => q.OrderByDescending(c => c.Name);
                    break;
                case "UrlSlug":
                    orderBy = q => q.OrderBy(c => c.UrlSlug);
                    break;
                case "urlSlug_desc":
                    orderBy = q => q.OrderByDescending(c => c.UrlSlug);
                    break;
                case "TotalPosts":
                    orderBy = q => q.OrderBy(c => c.Posts.Count);
                    break;
                case "totalPosts_desc":
                    orderBy = q => q.OrderByDescending(c => c.Posts.Count);
                    break;
                case "InsertedAt":
                    orderBy = q => q.OrderBy(c => c.InsertedAt);
                    break;
                case "insertedAt_desc":
                    orderBy = q => q.OrderByDescending(c => c.InsertedAt);
                    break;
                case "UpdatedAt":
                    orderBy = q => q.OrderBy(c => c.UpdatedAt);
                    break;
                case "updatedAt_desc":
                    orderBy = q => q.OrderByDescending(c => c.UpdatedAt);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Name);
                    break;
            }

            var categories = await _categoryServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(categories);
        }

        // GET: Admin/CategoryManagement/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/CategoryManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UrlSlug,Description,IsDeleted,InsertedAt,UpdatedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = Guid.NewGuid();
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/CategoryManagement/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoryManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UrlSlug,Description,IsDeleted,InsertedAt,UpdatedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/CategoryManagement/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoryManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}