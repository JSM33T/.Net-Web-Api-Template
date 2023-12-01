using almondCoveApi.Data;
using almondCoveApi.Enums;
using almondCoveApi.Models.Domain;
using almondCoveApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace almondCoveApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public readonly AlmondDbContext _context;
        public UserRepository(AlmondDbContext almondDbContext)
        {
            _context = almondDbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            await _context.UserProfiles.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<MatchUser> ExistsAsync(User user)
        {
            var existsByUsername = await _context.UserProfiles.AnyAsync(u => u.UserName == user.UserName);
            var existsByEmail = await _context.UserProfiles.AnyAsync(u => u.Email == user.Email);
            if (existsByUsername && existsByEmail)
            {
                // This condition shouldn't happen if both username and email are unique in your database.
                return MatchUser.UserName;
            }
            else if (existsByUsername)
            {
                return MatchUser.UserName;
            }
            else if (existsByEmail)
            {
                return MatchUser.Email;
            }
            else
            {
                return MatchUser.None;
            }

        }



        public Task<User> GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.UserProfiles.ToListAsync();
        }
    }
}
