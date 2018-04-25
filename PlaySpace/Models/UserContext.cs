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
    }
}