using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrueDogStore.Models;
using System.Runtime.CompilerServices;

namespace TrueDogStore.ViewModels
{
    public class CreatePetViewModel
    {
       
            [Key]
            public int Id { get; set; }
            [ForeignKey("Pet_Activity")]
            public int Pet_ActivityId { get; set; }
            [Required(ErrorMessage = "Pet Activity Name is required.")]
            public string PetActivityName { get; set; }
            [Required(ErrorMessage = "Pet Activity Description is required.")]
            public string PetActivityDescription { get; set; }
            [Required(ErrorMessage = "Activity Level Name is required.")]
            public string ActivityLevelName { get; set; }
            [Required(ErrorMessage = "Activity Level Description is required.")]
            public string ActivityLevelDescription { get; set; }
            [Required(ErrorMessage = "Size description is required.")]
            public string SizeDescription { get; set; }
            [ForeignKey("Breed")]
            public int BreedId { get; set; }
            [Required(ErrorMessage = "Breed Name is required.")]
            public string BreedName { get; set; }
            [Required(ErrorMessage = "Breed Description is required.")]
            public string BreedDescription { get; set; }
            [Required(ErrorMessage = "Breed Group Name is required.")]
            public string BreedBreed_GroupName { get; set; }
            [Required(ErrorMessage = "Breed Group Description is required.")]
            public string BreedBreed_GroupDescription { get; set; }
            [ForeignKey("Shelter")]
            public int ShelterId { get; set; }
            public Shelter? Shelter { get; set; }
            [Required(ErrorMessage = "Image is required.")]
            public IFormFile? ImagePath { get; set; }
            [Required(ErrorMessage = "Name is required.")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Fur length is required.")]  
            public string? Fur_Length { get; set; }
            [Required(ErrorMessage = "Color is required.")]
            public string? Color { get; set; }
            [Required(ErrorMessage = "Weight is required.")]  
            public float? Weight { get; set; }
            [Required(ErrorMessage = "Age is required.")]   
            public int? Age { get; set; }
            [Required(ErrorMessage = "Gender is required.")] 
            public string? Gender { get; set; }
            public bool? Human_Friendly { get; set; }
            public bool?  Pet_Friendly { get; set; }
            public bool? Apartment_Friendly { get; set; }
            public bool? Leash_Trained { get; set; }
            public bool? Litter_Trained { get; set; }
            public string? AppUserId { get; set; }   
        
    }
}
