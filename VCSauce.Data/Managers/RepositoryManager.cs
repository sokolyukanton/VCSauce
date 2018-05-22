using System;
using System.Collections.Generic;
using System.Linq;
using VCSauce.Data.Entities;
using Version = VCSauce.Data.Entities.Version;

namespace VCSauce.Data.Managers
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
            if(_db.Repositories.Any(r=>r.Path==path)) throw new ArgumentException("such path exist", nameof(path));

            System.IO.Directory.CreateDirectory(storagePath);
            var newRepo=new Repository
            {
                Path = path,
                StoragePath = storagePath,
                Name = !string.IsNullOrEmpty(name)?name:$"Repository {_db.Repositories.Count()+1}"
            };
            var initVersion=new Version
            {
                Date = DateTime.Now,
                Label = "Initial commit",
                Files = StorageManager.GetFilesFromDirectory(path).ToList()
            };
            StorageManager.InitialDirectoryToStorage(path, storagePath);
            newRepo.Versions.Add(initVersion);
            _db.Repositories.Add(newRepo);
            _db.SaveChanges();
        }

        public void RenameRepository(Repository repo)
        {
            _db.Repositories.Update(repo);
            _db.SaveChanges();
        }

        public void DeleteRepository(Repository repo)
        {
            _db.Repositories.Remove(repo);
            _db.SaveChanges();
        }

        public void ChangeRepositoryStorage(Repository repo,string newpath)
        {
            StorageManager.MoveDirectory(repo.StoragePath,newpath);
            repo.StoragePath= newpath;
            _db.Repositories.Update(repo);
            _db.SaveChanges();
        }

        public IEnumerable<Repository> GetRepositories()
        {
            return _db.Repositories;
        }
    }
}
