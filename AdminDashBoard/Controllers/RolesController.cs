using AdminDashBoard.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));
            
            var roleExist = await _roleManager.RoleExistsAsync(model.Name!);
            if (roleExist)
            {
                ModelState.AddModelError("Name", "Role Already Exist");
                return RedirectToAction(nameof(Index));
            }
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = model.Name! });

            if (result.Succeeded)
               TempData["SuccessMessage"] = "Role Created Successfully";    
            else
               TempData["ErrorMessage"] = "Failed to create role.";
            
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                TempData["ErrorMessage"] = "Role not found";
                return RedirectToAction(nameof(Index));
            }
            var VM = new UpdateRoleViewModel
            {
                Id = id,
                Name = role.Name!
            };
            return View(VM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id!);
                if (role is null)
                {
                    TempData["ErrorMessage"] = "Role not found";
                    return RedirectToAction(nameof(Index));
                }

                var roleExist = await _roleManager.FindByNameAsync(model.Name!);
                if (roleExist is not null && roleExist.Id != model.Id)
                {
                    ModelState.AddModelError("Name", "Role Already Exist");
                    return View(model);
                }

                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Role Edited Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is not null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                   TempData["ErrorMessage"] = "Failed to delete role";               
                else                 
                    TempData["SuccessMessage"] = "Role Deleted Successfully";              
            }
            else
            {
                TempData["ErrorMessage"] = "Role not found";
            }
            return RedirectToAction(nameof(Index));
        }



    }
}