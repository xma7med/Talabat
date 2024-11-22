using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Talabat.Dashboard.Controllers
{
    public class AdminController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invaild Email");
                return RedirectToAction(nameof(Login));
            }
            var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!Result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty, "You Are Not authorized");
                return RedirectToAction(nameof(Login));
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
