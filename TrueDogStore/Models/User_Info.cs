using TrueDogStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueDogStore.Models
{
    public class User_Info
    {
        [Key]
        public int Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public List<Pet> OwnedPets { get; set; }
        public string? Description { get; set; }
    }
}
