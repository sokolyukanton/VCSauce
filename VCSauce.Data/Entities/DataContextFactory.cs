using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VCSauce.Data.Entities
{
    class DataContextFactory:IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            string connectionString= @"Server=(localdb)\mssqllocaldb;Database=VCSauceDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(connectionString);
            return new DataContext(builder.Options);
        }
    }
}
