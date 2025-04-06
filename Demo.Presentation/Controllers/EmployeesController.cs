using Demo.BusinessLogic.DataTransferOpjects.DepartmentDto;
using Demo.BusinessLogic.DataTransferOpjects.Employee;
using Demo.BusinessLogic.Services.Employees;
using Demo.DataAccess.Moodels.EmployeeModel;
using Demo.DataAccess.Moodels.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService , IWebHostEnvironment _environment , ILogger<EmployeesController> logger) : Controller
    {
        public IActionResult Index()
        {
            var Employee = _employeeService.GetAllEmployees();
            return View(Employee);
        }
        #region create

        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeeService.CreateEmployee(employeeDto);
                    if (result == 0)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee can not be create");
                    }

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        logger.LogError(ex.Message);
                    }

                }
            }
            return View(employeeDto);
        }
        #endregion

        #region details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ? NotFound() : View(employee);  
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            var employeeDto = new UpdateEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),

            };
            return View(employeeDto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id,UpdateEmployeeDto employeeDto)
        {
            if (!id.HasValue || id != employeeDto.Id) return BadRequest();
            if(!ModelState.IsValid) return View(employeeDto);
            try
            {
                var Result = _employeeService.UpdateEmployee(employeeDto);
                if(Result > 0 )
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not Updated");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeDto);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
;
            }
        }
        #endregion

        #region delete
        [HttpPost]
        public IActionResult Delete(int id)
        { 
          if (id == 0) return BadRequest();

            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is not Deleted");
                    return RedirectToActionPermanent(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {

                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                };
            }

        }
        #endregion

    }
}
