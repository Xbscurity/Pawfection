using System.ComponentModel.DataAnnotations;

namespace TrueDogStore.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string? SelectedLoginProvider { get; set; }
    }
}
