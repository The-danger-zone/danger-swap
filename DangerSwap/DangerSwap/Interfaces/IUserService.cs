using DangerSwap.Models;
using System.Security.Claims;

namespace DangerSwap.Interfaces;

public interface IUserService
{
    Task UpdateUserInfo(UpdateableUser userInfo, ClaimsPrincipal claimsPrincipal);

    Task UpdatePassword(UpdateableUser userInfo, User user);

    Task UpdateEmail(UpdateableUser userInfo, User user);

    Task<User> GetUser(ClaimsPrincipal userClaim);
}
