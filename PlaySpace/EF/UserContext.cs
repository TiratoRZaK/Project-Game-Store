using System.Data.Entity;
using PlaySpace.Entities;

namespace PlaySpace.EF
{
    public class UserContext : DbContext
    {
        public DbSet<Game> Games { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrdGame> OrdGames { get; set; }
        public DbSet<ItemKey> ItemKeys { get; set; }
    }

}