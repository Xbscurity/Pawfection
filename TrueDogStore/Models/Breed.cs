using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueDogStore.Models
{
    public class Breed
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Breed_Group")]
        public int Breed_GroupId { get; set; }
        public Breed_Group Breed_Group { get; set; }
    }
}
