using DangerSwap.DbContexts;
using DangerSwap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DangerSwap.Repositories
{
    public class UserRepository
    {
        //Mindaugas
        private readonly UserManager<User> _userManager;
        private readonly DangerSwapContext _dbContext;

        public UserRepository(UserManager<User> userManager, DangerSwapContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IdentityResult> CreateEntity(User entity)
        {
            return await _userManager.CreateAsync(entity, entity.Password);
        }
//Mindaugas
        public async Task DeleteEntity(string id)
        {
            var user = _dbContext.Users.FirstOrDefault(t => t.Id == id);
            if (user = default)
            {
                 throw new ArgumentException("User deleted from repository:",nameof(user));
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetEntitiesAsync() => await _dbContext.Users.ToListAsync();
//Mindaugas
        public async Task<User?> GetEntity(string id) => await _dbContext.Users.FirstOrDefaultAsync(t => t.Id == id);
        public async Task<User?> GetEntityByUsername(string username) => await _dbContext.Users.FirstOrDefaultAsync(t => t.UserName == username);

        public async Task<bool> IsExist(string id) => await _dbContext.Users.AnyAsync(t => t.Id == id);

        public async Task UpdateEntity(User entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
