using Demo.BusinessLogic.Services;
using Demo.DataAccess.Moodels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController1(IDepartmentService departmentService) : Controller
    {
        public IActionResult Index()
        {
            var Departments = departmentService.GetAllDepartments();
            return View(Departments);
        }
    }
}
