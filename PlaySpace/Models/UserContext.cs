using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlaySpace.Models
{
    public class UserContext : DbContext
    {
        public DbSet<Game> Games { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Category> Categories { get; set; }


        public Game DeleteGame(int gameId)
        {
            UserContext context = new UserContext();
            Game dbEntry = context.Games.Find(gameId);
            if (dbEntry != null)
            {
                context.Games.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Category DeleteCategory(int categoryId)
        {
            UserContext context = new UserContext();
            Category dbEntry = context.Categories.Find(categoryId);
            foreach (Game item in context.Games)
            {
                if (item.CategoryId == categoryId)
                {
                    context.Games.Remove(item);
                }
            }
            if (dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveGame(Game game)
        {
            UserContext context = new UserContext();
            if (game.GameId == 0)
                context.Games.Add(game);
            else
            {
                Game dbEntry = context.Games.Include(nameof(Game.Keys)).FirstOrDefault(g => g.GameId == game.GameId);
                if (dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.Discription = game.Discription;
                    dbEntry.Price = game.Price;
                    dbEntry.Discount = game.Discount;
                    dbEntry.ImageData = game.ImageData;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                    dbEntry.Category = game.Category;
                    dbEntry.CategoryId = game.CategoryId;
                    if ((dbEntry.Keys.FirstOrDefault(p => p.Item == game.ActiveKey) == null) && (dbEntry.ActiveKey != game.ActiveKey))
                    {
                        dbEntry.Keys.Add(new Key
                        {
                            Item = game.ActiveKey,
                            GameId = game.GameId
                        });
                    }

                }
            }
            context.SaveChanges();
        }

        public void SaveCategory(Category category)
        {
            UserContext context = new UserContext();
            if (category.Id == 0)
                context.Categories.Add(category);
            else
            {
                Category dbEntry = context.Categories.FirstOrDefault(g => g.Id == category.Id);
                if (dbEntry != null)
                {
                    dbEntry.CategoryName = category.CategoryName;
                }
            }
            context.SaveChanges();
        }
    }

}