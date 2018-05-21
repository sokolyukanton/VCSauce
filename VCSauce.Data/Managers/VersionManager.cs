using VCSauce.Data.Entities;

namespace VCSauce.Data.Managers
{
    public class VersionManager
    {
        private readonly DataContext _db;

        public VersionManager()
        {
            _db = new DataContextFactory().CreateDbContext(new string[] { });
        }
    }
}