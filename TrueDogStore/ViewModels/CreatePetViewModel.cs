using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrueDogStore.Models;

namespace TrueDogStore.ViewModels
{
    public class CreatePetViewModel
    {
       
            [Key]
            public int Id { get; set; }
            [ForeignKey("Pet_Activity")]
            public int Pet_ActivityId { get; set; }
            public Pet_Activity? Pet_Activity { get; set; }
            [ForeignKey("Size")]
            public int SizeId { get; set; }
            public Size? Size { get; set; }
            [ForeignKey("Breed")]
            public int BreedId { get; set; }
            public Breed? Breed { get; set; }
            [ForeignKey("Shelter")]
            public int ShelterId { get; set; }
            public Shelter? Shelter { get; set; }
            public IFormFile? ImagePath { get; set; }
            public string? Name { get; set; }
            public string? Fur_Length { get; set; }
            public string? Color { get; set; }
            public float? Weight { get; set; }
            public int? Age { get; set; }
            public string? Gender { get; set; }
            public bool? Human_Friendly { get; set; }
            public bool? Pet_Friendly { get; set; }
            public bool? Apartment_Friendly { get; set; }
            public bool? Leash_Trained { get; set; }
            public bool? Litter_Trained { get; set; }
            public string? AppUserId { get; set; }
        
    }
}
