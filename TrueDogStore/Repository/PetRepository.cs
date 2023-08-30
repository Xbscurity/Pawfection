using Microsoft.EntityFrameworkCore;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;
        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Pet pet)
        {
            _context.Add(pet);
            return Save();

        }

        public bool Delete(Pet pet)
        {
            _context.Remove(pet);
            return Save();
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _context.Pets
                 .Include(i => i.Shelter)
                 .Include(i => i.Pet_Activity)
                 .ThenInclude(i => i.Activity_Level)
                 .Include(i => i.Size)
                 .Include(i => i.Breed)
                 .ThenInclude(i => i.Breed_Group)
                 .ToListAsync();
        }


        public async Task<Pet> GetByIdAsync(int id)
        {
            return await _context.Pets
                .Include(i => i.Shelter)
                .Include(i => i.Pet_Activity)
                .ThenInclude(i => i.Activity_Level)
                .Include(i => i.Size).Include(i => i.Breed)
                .ThenInclude(i => i.Breed_Group)
                .FirstOrDefaultAsync(i => i.Id == id);

        }

        public async Task<IEnumerable<Pet>> GetPetByShelter(string adress)
        {
            return await _context.Pets.Where(c => c.Shelter.Address.Contains(adress)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Pet pet)
        {
            _context.Update(pet);
            return Save();
        }
    }
}
