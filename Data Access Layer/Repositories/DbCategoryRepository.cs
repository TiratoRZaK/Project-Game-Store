using System;
using System.Collections.Generic;
using Data_Access_Layer.Entities;
using Data_Access_Layer.EF;
using Data_Access_Layer.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Data_Access_Layer.Repositories
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
                    db.SaveChanges();
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