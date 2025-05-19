
using Demo.DataAccess.Moodels.IdentityModel;
using Demo.Presentation.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicatonUser> _userManager) : Controller
    {
        public IActionResult Index(string? RoleSearchName)
        {

            var roles = _roleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(RoleSearchName))
            {
                roles = roles.Where(u => u.Name.Contains(RoleSearchName));
            }
            return View(roles.ToList());

        }

        [HttpGet]
        public IActionResult Details(string Id)
        {

            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            var roleDetails = _roleManager.FindByIdAsync(Id).Result;

            if (roleDetails is not null)
                return View(roleDetails);

            else return NotFound();


        }



        [HttpGet]
        public IActionResult Delete(string Id)
        {

            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            var deleteRole = _roleManager.FindByIdAsync(Id).Result;

            if (deleteRole is not null)
                return View(deleteRole);

            else return NotFound();
        }



        [HttpPost]
        public IActionResult Delete(string Id, IdentityRole _identityRole)
        {

            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            if (!ModelState.IsValid) return View(_identityRole);

            var role = _roleManager.FindByIdAsync(Id).Result;
            if (role is null)
                return NotFound();


            var Result = _roleManager.DeleteAsync(role).Result;

            if (Result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in Result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(_identityRole);
        }


        [HttpGet]
        public IActionResult Edit(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            var updateRole = _roleManager.FindByIdAsync(Id).Result;
            if (updateRole is not null)
                return View(updateRole);
            else
                return NotFound();
        }


        [HttpPost]
        public IActionResult Edit(string Id, IdentityRole _identityRole)
        {

            if (string.IsNullOrWhiteSpace(Id))
                return BadRequest();

            if (!ModelState.IsValid) return View(_identityRole);

            var role = _roleManager.FindByIdAsync(Id).Result;
            if (role is null)
                return NotFound();


            role.Name = _identityRole.Name;

            var Result = _roleManager.UpdateAsync(role).Result;

            if (Result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in Result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(_identityRole);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public IActionResult Create(CreateRoleViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var newRole = new IdentityRole { Name = model.Name };

            var result = _roleManager.CreateAsync(newRole).Result;

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
    }
    
}
