using Demo.DataAccess.Moodels.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees;

        public EmployeeService()
        {
            _employees = new List<Employee>();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddNewEmployee(Employee employee)
        {
            employee.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = GetEmployeeById(employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Age = employee.Age;
                existingEmployee.Address = employee.Address;
                existingEmployee.IsActive = employee.IsActive;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.HiringDate = employee.HiringDate;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.EmployeeType = employee.EmployeeType;
                existingEmployee.CreateBy = employee.CreateBy;
                existingEmployee.LastModifiedBy = employee.LastModifiedBy;
            }
        }
    }
}
