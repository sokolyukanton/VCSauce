using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCSauce.Data.Entities;
using VCSauce.Data.Managers;
using File = System.IO.File;

namespace DataTests
{
    [TestClass]
    public class VersionManagerTests
    {
        private readonly VersionManager _vm = new VersionManager();
        private readonly Repository _repository = new RepositoryManager().GetRepositories().First();

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VersionCreated_WithNoChanges()
        {
            _vm.CreateVersion(_repository);
            Assert.IsTrue(_vm.GetVersions(_repository).Last().Files.Count==0);
        }

        [TestMethod]
        public void VersionCreated_FileAdded()
        {
            var createdfile = Path.Combine(_repository.Path, $"{Guid.NewGuid().ToString("n").Substring(0, 4)}.txt");
            var stream=File.Create(createdfile);
            stream.Close();
            _vm.CreateVersion(_repository);
            Assert.IsTrue(_vm.GetVersions(_repository).Last().Files.Count(f=>f.State==State.New) >= 1);
        }

        [TestMethod]
        public void VersionCreated_FileChanged()
        {
            var filepath = Path.Combine(_repository.Path,
                _vm.GetVersions(_repository).Last().Files.Last().Path.TrimStart(Path.DirectorySeparatorChar));
            File.AppendAllText(filepath, DateTime.Now+Environment.NewLine);
            _vm.CreateVersion(_repository);
            Assert.IsTrue(_vm.GetVersions(_repository).Last().Files.Count(f => f.State == State.Changed) >= 1);
        }

        [TestMethod]
        public void VersionCreated_FileDeleted()
        {
            var filepath = Path.Combine(_repository.Path,
                _vm.GetVersions(_repository).Last().Files.Last().Path.TrimStart(Path.DirectorySeparatorChar));
            File.Delete(filepath);
            _vm.CreateVersion(_repository);
            Assert.IsTrue(_vm.GetVersions(_repository).Last().Files.Count(f => f.State == State.Deleted) >= 1);
        }

        [TestMethod]
        public void VersionRenamed()
        {
            // arrange
            var version = _vm.GetVersions(_repository).First();
            var oldreponame = version.Label;
            version.Label = Guid.NewGuid().ToString("n").Substring(0, 8);
            // act
            _vm.RenameVersion(version);
            // assert 
            Assert.AreNotSame(oldreponame, _vm.GetVersions(_repository).First().Label);
        }
    }
}
