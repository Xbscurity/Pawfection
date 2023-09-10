using Microsoft.EntityFrameworkCore;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDbContext _context;
        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Pet>> SearchBreeds(string searchTerm)
        { 
            
            return await _context.Pets.Include(p => p.Breed)
                .Where(p => p.Breed != null && p.Breed.Name
                .Contains(searchTerm.Trim()))
                .ToListAsync();
        }
    }
}
