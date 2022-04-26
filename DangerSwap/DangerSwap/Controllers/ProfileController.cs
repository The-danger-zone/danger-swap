using DangerSwap.Models;
using DangerSwap.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DangerSwap.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateableUser updateableUser)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if(updateableUser.NewEmail == updateableUser.ConfirmedNewEmail && updateableUser.ConfirmedNewEmail != user.Email)
                {
                    var token = await _userManager.GenerateChangeEmailTokenAsync(user, updateableUser.NewEmail);
                    await _userManager.ChangeEmailAsync(user, updateableUser.NewEmail, token);
                }
                if (updateableUser.OldPassword == user?.Password && updateableUser.NewPassword == updateableUser.ConfirmedNewPassword)
                {
                    await _userManager.ChangePasswordAsync(user, updateableUser.OldPassword, updateableUser.ConfirmedNewPassword);
                }
                return RedirectToAction(nameof(Index), "Converter");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
