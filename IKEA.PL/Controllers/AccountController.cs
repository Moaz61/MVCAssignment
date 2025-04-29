using IKEA.DAL.Models.IdentityModel;
using IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View(); 

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);

            var User = new ApplicationUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
            };

            var Result = _userManager.CreateAsync(User , viewModel.Password).Result;
            if(Result.Succeeded) 
                return RedirectToAction("Login");
            else
            {
                foreach(var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }

        }
        #endregion

        //Log In
        //Log Out
    }
}
