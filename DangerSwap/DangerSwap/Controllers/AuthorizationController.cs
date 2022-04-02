using DangerSwap.DbContexts;
using DangerSwap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DangerSwap.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DangerSwapContext _dbContext;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager, DangerSwapContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
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
               var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    return View(nameof(Login));
                }
                foreach(var error in result.Errors)
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
                var user = _dbContext.Users.First(q => q.Email == email);
                var identityResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("/", nameof(HomeController));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
            }
            return View(nameof(Login));
        }
    }
}
