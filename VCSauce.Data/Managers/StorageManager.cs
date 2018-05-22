using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using VCSauce.Data.Entities;
using File = VCSauce.Data.Entities.File;
using Type = VCSauce.Data.Entities.Type;

namespace VCSauce.Data.Managers
{
    public static class StorageManager
    {
       
        public static IEnumerable<File> GetFilesFromDirectory(string dirpath)
        {
            return Directory.EnumerateFiles(dirpath, "*.*", SearchOption.AllDirectories)
                .Select(filepath => new File
            {
                Path = Path.PathSeparator+filepath.Remove(0,dirpath.Length),
                State = State.New,
                Type = Type.Other,
                Hash = GetFileHash(filepath)
            });
        }

        public static List<File> GetActualForNewVersionFiles(List<File> currentFiles,string repositoryPath)
        {
            List<File> actualFiles = new List<File>();
            var filesList = Directory.EnumerateFiles(repositoryPath, "*.*", SearchOption.AllDirectories).ToList();
            foreach (var filepath in filesList)
            {
                if (currentFiles.Any(f => Path.Combine(repositoryPath,f.Path) == filepath))
                {
                    var newHash = GetFileHash(filepath);
                    var file=currentFiles.Single(f => Path.Combine(repositoryPath, f.Path) == filepath);
                    if (!file.Hash.Equals(newHash))
                    {
                        file.Hash = newHash;
                        file.State = State.Changed;
                    }
                }
                else
                {
                    actualFiles.Add(new File
                    {
                        Path = Path.PathSeparator + filepath.Remove(0, repositoryPath.Length),
                        State = State.New,
                        Type = Type.Other,
                        Hash = GetFileHash(filepath)
                    });
                }
            }

            foreach (var currentFile in currentFiles)
            {
                if (!filesList.Contains(Path.Combine(repositoryPath, currentFile.Path)))
                {
                    currentFile.State = State.Deleted;
                }
            }
            return actualFiles;
        }

        public static void MoveDirectory(string oldpath, string newpath)
        {
            Directory.Move(oldpath,newpath);
        }

        public static void InitialDirectoryToStorage(string dirpath, string storagePath)
        {
            var files = Directory.EnumerateFiles(dirpath, "*.*", SearchOption.AllDirectories).ToList();
            using (ZipArchive archive=ZipFile.Open(Path.Combine(storagePath, "Version1.zip"),ZipArchiveMode.Update))
            {
                foreach (var filepath in files)
                {
                    archive.CreateEntryFromFile(filepath, new FileInfo(filepath).Name, CompressionLevel.Optimal);
                }
            }
        }

        public static void MoveFilesToStorage(List<File> files, string repositoryPath, string storagePath,int versionNumber)
        {
            using (ZipArchive archive = ZipFile.Open(Path.Combine(storagePath, $"Version{versionNumber}.zip"), ZipArchiveMode.Update))
            {
                foreach (var file in files)
                {
                    if (file.State != State.Deleted)
                    {
                        string filepath=Path.Combine(repositoryPath,file.Path);
                        archive.CreateEntryFromFile(filepath, new FileInfo(filepath).Name, CompressionLevel.Optimal);
                    }
                }
            }
            
        }

        private static byte[] GetFileHash(string fileName)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return sha1.ComputeHash(stream);
        }

    }
}
