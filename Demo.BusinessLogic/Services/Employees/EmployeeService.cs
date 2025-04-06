using AutoMapper;
using Demo.BusinessLogic.DataTransferOpjects.Employee;
using Demo.DataAccess.Moodels.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public class EmployeeService(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking)
        {
            var Employees = _employeeRepository.GetAll(WithTracking = false);

            var employeesDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);
            return employeesDto;
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
        }
        public EmployeeDetialDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);

            return employee is null ? null : _mapper.Map<Employee,EmployeeDetialDto>(employee);
        }
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreateEmployeeDto , Employee>(employeeDto);
            return _employeeRepository.Add(employee);
        }
        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(_mapper.Map<UpdateEmployeeDto , Employee>(employeeDto));
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
               return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }



    }
}
