using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostRegister(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var user = new ApplicationUser
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };

            var result = _userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Result.Succeeded)
            {
                foreach(var error in result.Result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }

                return View(registerViewModel);
            }
            return RedirectToAction("Home", "Index");
        }
    }
}
