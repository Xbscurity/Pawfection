using Microsoft.EntityFrameworkCore;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Repository
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly ApplicationDbContext _context;

        public ShelterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Shelter> GetByIdAsync(int id)
        {
            return await _context.Shelters.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Shelter> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Shelters.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<List<Shelter>> GetAll()
        {
            return await _context.Shelters.AsNoTracking().ToListAsync();
        }
    }
}
