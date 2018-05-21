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
        RepositoryManager rm = new RepositoryManager();
        [TestMethod]
        public void RepositoryCreated()
        {
            // arrange
            var path = @"I:\ВУЗ\Диплом";
            var storagepath = @"I:\ВУЗ\Диплом\Storage";
            // act
            try
            {
                rm.CreateRepository(path, storagepath);
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
            rm.CreateRepository(path, storagepath);
            rm.CreateRepository(path, storagepath);
            // assert 
            
        }

        [TestMethod]
        public void RepositoryRenamed()
        {
            // arrange
            var repo = rm.GetRepositories().First();
            var oldreponame = repo.Name;
            repo.Name = Guid.NewGuid().ToString("n").Substring(0, 8); 
            // act
            rm.RenameRepository(repo);
            // assert 
            Assert.AreNotSame(oldreponame, rm.GetRepositories().First().Name);
        }

        [TestMethod]
        public void RepositoryDeleted()
        {
            // arrange
            var path = @"I:\ВУЗ\Диплом\Storage";
            var storagepath = @"I:\ВУЗ\Диплом\Storage\Storage";
            Directory.Delete(path,true);
            // act
            rm.CreateRepository(path, storagepath);
            rm.DeleteRepository(rm.GetRepositories().Single(r=>r.Path==path));
            // assert 
            Assert.IsFalse(rm.GetRepositories().Any(r => r.Path == path));
        }
    }
}
