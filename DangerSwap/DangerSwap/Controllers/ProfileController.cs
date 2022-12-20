using DangerSwap.Interfaces;
using DangerSwap.Models;
using Microsoft.AspNetCore.Mvc;

namespace DangerSwap.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateableUser updateableUser)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            await _userService.UpdateUserInfo(updateableUser, User);

            return RedirectToAction(nameof(Index), "Converter");
        }
    }
}
