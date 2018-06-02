using PlaySpace.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlaySpace.Models
{
    public class DbCategoryRepository : ICategoryRepository
    {
        UserContext db = new UserContext();

        public DbCategoryRepository()
        {
            this.db = new UserContext();
        }

        public IEnumerable<Category> GetCategoryList()
        {
            return db.Categories;
        }

        public Category GetCategory(int id)
        {
            return db.Categories.Find(id);
        }

        public Category Create(Category item)
        {
            db.Categories.Add(item);
            return item;
        }

        public void Update(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public Category Delete(int categoryId, IGameRepository repository)
        {            
            Category dbEntry = db.Categories.Find(categoryId);
            foreach (Game item in repository.GetGameList())
            {
                if (item.CategoryId == categoryId)
                {
                    repository.Delete(item.GameId);
                }
            }
            if (dbEntry != null)
            {
                db.Categories.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}