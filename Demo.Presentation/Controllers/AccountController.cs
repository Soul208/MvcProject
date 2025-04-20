using Demo.DataAccess.Moodels.IdentityModel;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicatonUser> _userManager 
        , SignInManager<ApplicatonUser> _signInManager) : Controller
    {
        //register
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);


            var User = new ApplicatonUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,

            };
          var Result = _userManager.CreateAsync(User , viewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }
        }

        #endregion

        #region Login

        [HttpGet]

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            
            var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if(User is not null)
            {
               bool Flag =  _userManager.CheckPasswordAsync(User, viewModel.Password).Result;
                if (Flag)
                {
                    var Result = _signInManager.PasswordSignInAsync(User, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account Is Loced Out");
                    if(Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
                return View(viewModel);
        }

        #endregion
    }
}
