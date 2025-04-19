using AutoMapper;
using Demo.BusinessLogic.DataTransferOpjects.Employee;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.DataAccess.Moodels.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public class EmployeeService(IunitOfWork _unitOfWork
        , IMapper _mapper , 
        IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            //var Employees = _employeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            //var employeesDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);
            //return employeesDto;
            IEnumerable<Employee> employee;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employee = _unitOfWork.EmployeeRepository.GetAll();
            else
                employee = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
                var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employee);
                return employeesDto;

            #region manual
            //var employeesDto = Employees.Select(Emp => new EmployeeDto()
            //{
            //    Id = Emp.Id,
            //    Name = Emp.Name,
            //    Age = Emp.Age,
            //    Email = Emp.Email,
            //    IsActive = Emp.IsActive,
            //    Salary = Emp.Salary,
            //    EmployeeType = Emp.EmployeeType.ToString(),
            //    Gender = Emp.Gender.ToString(),

            //});return employeesDto;
            #endregion


        }
        public EmployeeDetialDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);

            return employee is null ? null : _mapper.Map<Employee,EmployeeDetialDto>(employee);
        }
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreateEmployeeDto , Employee>(employeeDto);

            if (employeeDto.Image != null)
            {

                employee.ImageName = _attachmentService.Upload(employeeDto.Image , "Images");
             

            }




             _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.SaveChanges();
        }
        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
             _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdateEmployeeDto , Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false; ;
            }
        }



    }
}
