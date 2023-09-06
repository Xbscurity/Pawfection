using TrueDogStore.Models;

namespace TrueDogStore.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Pet>> GetAllUserPets();
    }
}
