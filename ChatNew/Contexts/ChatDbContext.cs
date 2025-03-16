using ChatNew.Models.DataBase;
using Microsoft.EntityFrameworkCore;


namespace ChatNew.Contexts
{
    class ChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public ChatDbContext ()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=Chat.db");
        }
    }
}
