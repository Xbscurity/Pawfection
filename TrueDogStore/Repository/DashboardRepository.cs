using Microsoft.EntityFrameworkCore;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Pet>>  GetAllUserPets()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userPets =  _context.Pets
                .Include(i => i.Shelter)
                .Include(i => i.Pet_Activity)
                .ThenInclude(i => i.Activity_Level)
                .Include(i => i.Size).Include(i => i.Breed)
                .ThenInclude(i => i.Breed_Group)
                .Where(r => r.appUser.Id == curUser.ToString());
            return userPets.ToList();
       }
    }
}
