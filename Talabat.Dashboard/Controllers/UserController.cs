using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Controllers
{
    public class UserController (RoleManager <IdentityRole> _roleManager , UserManager<ApplicationUser> _userManager): Controller
    {

        // No response  try to fix it 
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(u => new UserViweModel()
            { 
                Id           = u.Id,
                DisplayName  = u.DisplayName, 
                UserName     = u.UserName ,
                PhoneNumber  = u.PhoneNumber  ,
                Email        = u.Email,
                Roles        = _userManager.GetRolesAsync(u).Result,
            }).ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var allRoles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRoleViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = new List<RoleViewModel>()
            };

            foreach (var role in allRoles)
            {
                var roleViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                };

                viewModel.Roles.Add(roleViewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                // Remove role if it is selected but no longer checked
                if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                // Add role if it is not selected but has been checked
                if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
