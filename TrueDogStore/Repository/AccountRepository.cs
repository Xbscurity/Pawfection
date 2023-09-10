using Microsoft.EntityFrameworkCore;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;

namespace TrueDogStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountRepository(ApplicationDbContext context
            ,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByIdAsyncNoTracking(string id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
        _context.Users.Update(user);
        return Save();     
        }
        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
