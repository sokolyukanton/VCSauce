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
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.Entity<File>().Property(u => u.State).HasDefaultValue(State.New);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=VCSauceDb;Trusted_Connection=True;");
        }
    }
}
