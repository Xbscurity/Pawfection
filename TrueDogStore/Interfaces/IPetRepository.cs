using TrueDogStore.Models;

namespace TrueDogStore.Interfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAll();
        Task<Pet> GetByIdAsync(int id);
        Task<Pet> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Pet>> GetPetByShelter(string shelter);
        bool Add(Pet pet);
        bool Update(Pet pet);
        bool Delete(Pet pet);
        bool Save();
    }
}
