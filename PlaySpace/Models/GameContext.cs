using System.Data.Entity;


namespace PlaySpace.Models
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}