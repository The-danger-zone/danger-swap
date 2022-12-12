using DangerSwap.Models;
using Microsoft.AspNetCore.Identity;
using DangerSwap.Interfaces;
using System.Security.Claims;

namespace DangerSwap.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task UpdateUserInfo(UpdateableUser userInfo, ClaimsPrincipal claimsPrincipal)
    {
        var user = await GetUser(claimsPrincipal);
        await UpdateEmail(userInfo, user);
        await UpdatePassword(userInfo, user);
    }

    public async Task UpdateEmail(UpdateableUser userInfo, User user)
    {
        if (userInfo.NewEmail != userInfo.ConfirmedNewEmail || userInfo.ConfirmedNewEmail == user.Email) return;

        var token = await _userManager.GenerateChangeEmailTokenAsync(user, userInfo.NewEmail);
        await _userManager.ChangeEmailAsync(user, userInfo.NewEmail, token);
    }

    public async Task UpdatePassword(UpdateableUser userInfo, User user)
    {
        if (userInfo.OldPassword != user?.Password || userInfo.NewPassword != userInfo.ConfirmedNewPassword) return;

        await _userManager.ChangePasswordAsync(user, userInfo.OldPassword, userInfo.ConfirmedNewPassword);
    }

    public async Task<User> GetUser(ClaimsPrincipal userClaim)
    {
        var user = await _userManager.GetUserAsync(userClaim);
        return user;
    }
}
