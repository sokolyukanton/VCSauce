using System;
using System.Linq;
using VCSauce.Data.Entities;
using Version = VCSauce.Data.Entities.Version;

namespace VCSauce.Data.Services
{
    public class RepositoryManager
    {
        private readonly DataContext _db;

        public RepositoryManager()
        {
            _db = new DataContextFactory().CreateDbContext(new string[] { });
        }

        ///<exception cref = "ArgumentNullException" >Thrown if path or storagePath is null</exception >
        public void CreateRepository(string path,string storagePath,string name="")
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (storagePath == null) throw new ArgumentNullException(nameof(storagePath));

            System.IO.Directory.CreateDirectory(storagePath);
            var storageManager = new StorageManager(storagePath);
            var newRepo=new Repository
            {
                Path = path,
                StoragePath = storagePath,
                Name = !string.IsNullOrEmpty(name)?name:$"Repository{_db.Repositories.Count()+1}"
            };
            var initVersion=new Version
            {
                Date = DateTime.Now,
                Label = "Initial commit",
                Files = storageManager.GetFilesFromDirectory(path).ToList()
            };
            newRepo.Versions.Add(initVersion);
            _db.Repositories.Add(newRepo);
            _db.SaveChanges();
        }
    }
}
