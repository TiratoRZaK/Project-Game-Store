using System.Data.Entity;
using Data_Access_Layer.Entities;

namespace Data_Access_Layer.EF
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