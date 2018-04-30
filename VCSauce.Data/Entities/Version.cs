using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VCSauce.Data.Entities
{
    public class Version
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public List<File> Files { get; set; }=new List<File>();
    }
}
