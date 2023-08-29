using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TrueDogStore.Models
{
    public class Breed_Group
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
