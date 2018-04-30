using System.Collections.Generic;
using System.IO;
using System.Linq;
using VCSauce.Data.Entities;
using File = VCSauce.Data.Entities.File;
using Type = VCSauce.Data.Entities.Type;

namespace VCSauce.Data.Services
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

        public IEnumerable<File> GetFilesFromDirectory(string path)
        {
            return Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Select(filepath => new File
            {
                Path = filepath,
                State = State.New,
                Type = Type.Other
            });
        }
    }
}
