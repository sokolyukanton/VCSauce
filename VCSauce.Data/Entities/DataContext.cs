using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VCSauce.Data.Entities
{
    public class DataContext:DbContext
    {
        public DbSet<File> Files { get; set; }

        public DbSet<Version> Versions { get; set; }

        public DbSet<Repository> Repositories { get; set; }
        
        public DataContext(DbContextOptions options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                //Database.EnsureDeleted();
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Repository>().HasIndex(r => r.Path).IsUnique(true);
        }
    }
}
