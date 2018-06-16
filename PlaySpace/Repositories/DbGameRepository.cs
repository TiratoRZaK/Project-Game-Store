using System;
using System.Collections.Generic;
using PlaySpace.Entities;
using PlaySpace.EF;
using PlaySpace.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlaySpace.Repositories
{
    public class DbGameRepository : IGameRepository
    {
        UserContext db = new UserContext();

        public DbGameRepository()
        {
            this.db = new UserContext();
        }

        public IEnumerable<Game> GetGameList()
        {
            return db.Games;
        }

        public Game GetGame(int gameId)
        {
            return db.Games.Find(gameId);
        }

        public void Create(Game item)
        {
            db.Games.Add(item);
            db.SaveChanges();
        }

        public void Update(Game game)
        {
            Game dbEntry = db.Games.Include(nameof(Game.Keys)).FirstOrDefault(g => g.GameId == game.GameId);
            if (dbEntry != null)
            {
                dbEntry.Name = game.Name;
                dbEntry.Discription = game.Discription;
                dbEntry.Price = game.Price;
                dbEntry.Discount = game.Discount;
                if (game.ImageData != null && game.ImageMimeType != null)
                {
                    dbEntry.ImageData = game.ImageData;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                }
                dbEntry.Category = game.Category;
                dbEntry.CategoryId = game.CategoryId;

                if ((dbEntry.Keys.FirstOrDefault(p => p.Item == game.ActiveKey) == null))
                {
                    dbEntry.Keys.Add(new Key
                    {
                        Item = game.ActiveKey,
                        GameId = game.GameId
                    });
                    if (db.Keys.Where(m => m.Item == game.ActiveKey).Count() < 1)
                    {
                        dbEntry.CountKeys++;
                    }
                }
            }
            db.SaveChanges();
        }

        public Game Delete(int gameId)
        {
            Game game = db.Games.Find(gameId);
            if (game != null)
                db.Games.Remove(game);
            db.SaveChanges();
            return game;
        }

        public void Save()
        {
            db.SaveChanges();
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
