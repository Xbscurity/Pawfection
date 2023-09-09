using Microsoft.EntityFrameworkCore;
using TrueDogStore.Models;

namespace TrueDogStore.Interfaces
{
    public interface IShelterRepository
    {
        Task<Shelter> GetByIdAsync(int id);
        Task<Shelter> GetByIdAsyncNoTracking(int id);
        Task<List<Shelter>> GetAll();
    }
}
