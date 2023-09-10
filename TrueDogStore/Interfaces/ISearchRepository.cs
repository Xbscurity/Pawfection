using System.Reflection.Metadata;
using TrueDogStore.Models;

namespace TrueDogStore.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<Pet>> SearchBreeds(string searchTerm);
    }
}
