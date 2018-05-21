using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VCSauce.Data.Entities;
using File = VCSauce.Data.Entities.File;
using Type = VCSauce.Data.Entities.Type;

namespace VCSauce.Data.Managers
{
    public class StorageManager
    {
        public readonly string StroragePath;

        public StorageManager()
        {
        }

        public StorageManager(string stroragePath)
        {
            StroragePath = stroragePath;
        }

        public IEnumerable<File> GetFilesFromDirectory(string dirpath)
        {
            return Directory.EnumerateFiles(dirpath, "*.*", SearchOption.AllDirectories)
                .Select(filepath => new File
            {
                Path = Path.PathSeparator+filepath.Remove(0,dirpath.Length),
                State = State.New,
                Type = Type.Other
            });
        }

        public void CopyFilesToDirectory(List<File> files, string pathFrom, string pathTo)
        {
            
        }

        public void InitialDirectoryToStorage(string dirpath, string storagePath)
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

        
    }
}
