using DangerSwap.Models;
using Microsoft.AspNetCore.Identity;

namespace DangerSwap.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult> CreateEntity(User entity);

    Task DeleteEntity(string id);

    Task<IEnumerable<User>> GetEntitiesAsync();

    Task<User> GetEntity(string id);

    Task<User> GetEntity(string email, string password);

    Task<User?> GetEntityByUsername(string username);

    Task<bool> IsExist(string id);

    Task UpdateEntity(User entity);
}
