using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrueDogStore.Models;

namespace TrueDogStore.Models
{
    public class User : IdentityUser
    {
        public string? Image { get;set; }
        public ICollection<Pet> OwnedPets { get; set; }

    }
}
