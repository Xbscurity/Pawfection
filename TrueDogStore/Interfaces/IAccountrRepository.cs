using TrueDogStore.Models;

namespace TrueDogStore.Interfaces
{
    public interface IAccountRepository
    {
        ValueTask<AppUser> GetUserByIdAsync(string id);
        Task<AppUser> GetUserByIdAsyncNoTracking(string id);
        bool Update(AppUser user);
        bool Save();

    }
}
