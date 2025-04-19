using Demo.BusinessLogic.DataTransferOpjects;
using Demo.BusinessLogic.DataTransferOpjects.DepartmentDto;
using Demo.BusinessLogic.Services.DepartmentsServies;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Demo.Presentation.Controllers
{

    public class DepartmentController(IDepartmentService _departmentService ,ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment) : Controller 
    {
        
        public IActionResult Index()
        {
            ViewData["Message"] = "Hello From View Data";
            ViewBag.Message = "Hello from view Bag";
            var Departments = _departmentService.GetAllDepartments();
            return View(Departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModle departmentViewModle)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreateDepartmentDto()
                    {
                        Name = departmentViewModle.Name,
                        Code = departmentViewModle.Code,
                        DateOfCreation = departmentViewModle.DateOfCreation,
                        Description = departmentViewModle.Description,
                    };
                  int result =  _departmentService.CreateDepartment(departmentDto);
                    string Message;
                    if (result > 0)
                        Message = $"Department {departmentViewModle.Name} Is Created Succesfully";
                    else
                        Message = $"Department {departmentViewModle.Name} Can not Be Created";

                    TempData["Message"] = Message;
                    return RedirectToAction(nameof(Index));

                 
                }
                catch (Exception ex)
                {
                    if(_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                    
                }
            }
                return View(departmentViewModle);
        }
        #endregion

        #region details of department
        [HttpGet]
        public IActionResult Details(int? id)
        { 
         if(!id.HasValue) return BadRequest();
         var department = _departmentService.GetDepartmentById(id.Value);
        if (department == null) return NotFound();
        return View(department);
        }
        #endregion

        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        { 
         if (!id.HasValue) return BadRequest();
            var depatrment = _departmentService.GetDepartmentById(id.Value);
            if(depatrment == null) return NotFound();
            var departmentViewModel = new DepartmentViewModle()
            {
                Code = depatrment.Code,
                Name = depatrment.Name,
                Description = depatrment.Description,
                DateOfCreation = depatrment.DateOfCreation
            };
            return View(departmentViewModel);

        
        
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id , DepartmentViewModle viewModle)
        { 
            if(ModelState.IsValid)
            {
                try
                {
                    var UpdateDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id.Value,
                        Code = viewModle.Code,
                        Name = viewModle.Name,
                        DateOfCreation = viewModle.DateOfCreation,
                        Description = viewModle.Description,
                    };
                    int result = _departmentService.UpdateDepartment(UpdateDepartment);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not updated");
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
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }

                }

            }
            return View(viewModle);
           
        }

        #endregion

        #region Delete 

       // [HttpGet]

        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department == null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try
            {
              bool Deleted = _departmentService.DeleteDepartment(id);
                if(Deleted) 
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is not Deleted");
                    return RedirectToActionPermanent(nameof(Delete) , new {id});
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
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                };
            }
        }
        #endregion
    }
}
