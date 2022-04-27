using DangerSwap.DbContexts;
using DangerSwap.Models;
using DangerSwap.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DangerSwap.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        public AuthorizationController(UserRepository userRepository, SignInManager<User> signInManager)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.CreateEntity(user);
                if (result.Succeeded)
                {
                    return Redirect("/Authorization/Login");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(nameof(Registration));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetEntity(email, password);
                var identityResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (identityResult.Succeeded)
                {
                    return Redirect("/");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
            }
            return View(nameof(Login));
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
