using TrueDogStore.Models;

namespace TrueDogStore.ViewModels
{
    public class SearchResultViewModel
    {
        public int Id { get; set; }
        public Shelter? Shelter { get; set; }
        public Breed? Breed { get; set; }
        public string? ImagePath { get; set; }
    }
}
