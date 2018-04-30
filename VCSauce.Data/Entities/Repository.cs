using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VCSauce.Data.Entities
{
    public class Repository
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string StoragePath { get; set; }

        public List<Version> Versions { get; set; }=new List<Version>();
    }
}
