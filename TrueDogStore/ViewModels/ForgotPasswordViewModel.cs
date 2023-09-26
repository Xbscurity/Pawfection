using System.ComponentModel.DataAnnotations;

namespace TrueDogStore.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
