using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObject.IdentityDtos;

namespace AdminDashBoard.Controllers
{
    public class AccountController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager)
        : Controller
    {

            [HttpGet]
            public IActionResult Login() => View();


            [HttpPost]
            public async Task<IActionResult> Login(LoginDto model)
            {
                if (!ModelState.IsValid) return View(model);
                
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    // (Role Check)
                    if (await _userManager.IsInRoleAsync(user, "Admin")) 
                      return RedirectToAction(nameof(Index), "Home");
                    else
                    {  
                        await _signInManager.SignOutAsync();
                        ModelState.AddModelError("", "You are not authorized to access the Admin Panel.");
                        return View(model);
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(Login));
            }


    }
}
