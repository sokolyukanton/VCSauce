using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VCSauce.Data.Entities;
using Version = VCSauce.Data.Entities.Version;

namespace VCSauce.Data.Managers
{
    public class VersionManager
    {
        private readonly DataContext _db;

        public VersionManager()
        {
            _db = new DataContextFactory().CreateDbContext(new string[] { });
        }

        public void CreateVersion(Repository repository, string name)
        {
            List<File> latestFiles=new List<File>();
            var repo=_db.Repositories.Include(r=>r.Versions).ThenInclude(v=>v.Files).Single(r=>r.Id==repository.Id);
            foreach (var repoVersion in repo.Versions)
            {
                foreach (var versionFile in repoVersion.Files)
                {
                    switch (versionFile.State)
                    {
                        case State.New:
                            latestFiles.Add(versionFile);
                            break;
                        case State.Deleted:
                            latestFiles.RemoveAll(file=>file.Id==versionFile.Id);
                            break;
                        case State.Changed:
                            //replace file
                            latestFiles.RemoveAll(file => file.Id == versionFile.Id);
                            latestFiles.Add(versionFile);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            var actualFiles = StorageManager.GetActualForNewVersionFiles(latestFiles,repo.Path);
            StorageManager.MoveFilesToStorage(actualFiles,repo.Path,repo.StoragePath, repo.Versions.Count + 1);
            var newversion=new Version
            {
                Date = DateTime.Now,
                Label = !string.IsNullOrEmpty(name) ? name : $"Version {repo.Versions.Count + 1}",
                Files = actualFiles
            };
            repo.Versions.Add(newversion);
            _db.Repositories.Update(repo);
            _db.SaveChanges();
        }
    }
}