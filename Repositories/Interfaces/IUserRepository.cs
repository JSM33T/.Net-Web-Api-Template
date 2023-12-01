using almondCoveApi.Enums;
using almondCoveApi.Models.Domain;
using almondCoveApi.Models.DTO;

namespace almondCoveApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid id);
        Task<MatchUser> ExistsAsync(User user);
    }
}
