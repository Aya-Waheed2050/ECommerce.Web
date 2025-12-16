using AdminDashBoard.Models.Roles;
using AdminDashBoard.Models.Users;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UsersController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles,
                });
            }
            return View(userViewModels);
        }

        #region Edit (Get)
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return RedirectToAction(nameof(Index));

            var roles = await _roleManager.Roles.ToListAsync();

            var userModel = new UserRoleViewModel
            {
                UserId = user.Id!,
                UserName = user.DisplayName,
                Roles = new List<UpdateRoleViewModel>()
            };
            foreach (var role in roles)
            {
                var isSelected = await _userManager.IsInRoleAsync(user, role.Name!);
                userModel.Roles.Add(new UpdateRoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsSelected = isSelected,
                });
            }
            return View(userModel);
        }
        #endregion

        #region Edit (Post)

        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId!);
            if (user == null) return RedirectToAction(nameof(Index));

            var rolesForUser = await _userManager.GetRolesAsync(user);
            var tasks = new List<Task>();

            foreach (var role in model.Roles)
            {
                bool isUserInRole = rolesForUser.Any(r => r == role.Name);
                if (isUserInRole && !role.IsSelected)
                    tasks.Add(_userManager.RemoveFromRoleAsync(user, role.Name!));

                if (!isUserInRole && role.IsSelected)
                    tasks.Add(_userManager.AddToRoleAsync(user, role.Name!));
            }
            await Task.WhenAll(tasks);
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}