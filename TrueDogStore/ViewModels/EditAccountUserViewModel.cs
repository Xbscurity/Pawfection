namespace TrueDogStore.ViewModels
{
    public class EditAccountUserViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public IFormFile? Image { get; set; }

    }
}
