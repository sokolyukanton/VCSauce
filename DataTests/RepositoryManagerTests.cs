using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCSauce.Data.Managers;

namespace DataTests
{
    [TestClass]
    public class RepositoryManagerTests
    {
        private readonly RepositoryManager _rm = new RepositoryManager();

        [TestMethod]
        public void RepositoryCreated()
        {
            // arrange
            var path = @"I:\ВУЗ\Диплом";
            var storagepath = @"I:\ВУЗ\Диплом\Storage";
            // act
            try
            {
                _rm.CreateRepository(path, storagepath);
            }
            catch (ArgumentException e)
            {
            }
            // assert 
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RepositoryCreatedTwice()
        {
            // arrange
            var path = @"I:\ВУЗ\Диплом";
            var storagepath = @"I:\ВУЗ\Диплом\Storage";
            // act
            _rm.CreateRepository(path, storagepath);
            _rm.CreateRepository(path, storagepath);
            // assert 
            
        }

        [TestMethod]
        public void RepositoryRenamed()
        {
            // arrange
            var repo = _rm.GetRepositories().First();
            var oldreponame = repo.Name;
            repo.Name = Guid.NewGuid().ToString("n").Substring(0, 8); 
            // act
            _rm.RenameRepository(repo);
            // assert 
            Assert.AreNotSame(oldreponame, _rm.GetRepositories().First().Name);
        }

        [TestMethod]
        public void RepositoryDeleted()
        {
            // arrange
            var path = @"I:\ВУЗ\Диплом\Storage";
            var storagepath = @"I:\ВУЗ\Диплом\Storage\Storage";
            // act
            Directory.Delete(path, true);
            _rm.CreateRepository(path, storagepath);
            _rm.DeleteRepository(_rm.GetRepositories().Single(r=>r.Path==path));
            // assert 
            Assert.IsFalse(_rm.GetRepositories().Any(r => r.Path == path));
        }

        [TestMethod]
        public void RepositoryStorageChanged()
        {
            // arrange
            var repo = _rm.GetRepositories().First();
            var previouspath = repo.StoragePath;
            var newstoragepath = @"I:\ВУЗ\Диплом\NewStorage";
            // act
            _rm.ChangeRepositoryStorage(repo,newstoragepath);
            // assert 
            Assert.AreEqual(newstoragepath,_rm.GetRepositories().First().StoragePath);
            _rm.ChangeRepositoryStorage(_rm.GetRepositories().First(),previouspath);
        }
    }
}
