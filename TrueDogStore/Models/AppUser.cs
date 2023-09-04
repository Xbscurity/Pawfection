using TrueDogStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TrueDogStore.Models
{
    public class AppUser : IdentityUser
    {
        public string? Image { get; set; }

        public ICollection<Pet> OwnedPets { get; set; }

    }
}
