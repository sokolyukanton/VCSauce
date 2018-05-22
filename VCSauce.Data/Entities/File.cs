using System.ComponentModel.DataAnnotations;

namespace VCSauce.Data.Entities
{
    public class File
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public Type Type { get; set; }

        [Required]
        public State State { get; set; }

        [Required]
        public byte[] Hash { get; set; }

        public override string ToString()
        {
            return $"{nameof(Path)}: {Path}, {nameof(Type)}: {Type}, {nameof(State)}: {State}";
        }
    }

    public enum State
    {
        New=1,
        Deleted=2,
        Changed=3
    }

    public enum Type
    {
        Text=1,
        Other=2
    }
}
