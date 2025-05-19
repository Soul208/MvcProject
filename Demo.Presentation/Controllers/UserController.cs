
using Demo.DataAccess.Moodels.IdentityModel;
using Demo.presentation.ViewModels.User;
using Demo.Presentation.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class UserController(UserManager<ApplicatonUser> _userManager, SignInManager<ApplicatonUser> _signInManager) : Controller
    {
        public IActionResult Index(string? UserSearchName)
        {
            var users = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(UserSearchName))
            {
                users = users.Where(u => u.UserName.Contains(UserSearchName));
            }
            return View(users.ToList());
        }




        [HttpGet]
        public IActionResult Details(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            var user = _userManager.FindByIdAsync(Id).Result;
            if (user is null)
                return NotFound();

            var viewModel = new UserDetailsDto
            {
                Id = user.Id,
                FName = user.FirstName,
                LName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Edit(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            var user = _userManager.FindByIdAsync(Id).Result;
            if (user is null)
                return NotFound();

            var viewModel = new EditDto
            {
                Id = user.Id,
                FName = user.FirstName,
                LName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            return View(viewModel); 
        }


        [HttpPost]
        public IActionResult Delete(string Id, UserDetailsDto model)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            var user = _userManager.FindByIdAsync(Id).Result;
            if (user == null)
                return NotFound();

            var result = _userManager.DeleteAsync(user).Result;

            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            var user = _userManager.FindByIdAsync(Id).Result;
            if (user == null)
                return NotFound();

            var userDto = new UserDetailsDto
            {
                Id = user.Id,
                FName = user.FirstName,
                LName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(userDto);
        }
    }
}