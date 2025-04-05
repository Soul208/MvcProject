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
            var Employees = GetEmployeeById(employee.Id);
            if (Employees != null)
            {
                Employees.Name = employee.Name;
                Employees.Age = employee.Age;
                Employees.Address = employee.Address;
                Employees.IsActive = employee.IsActive;
                Employees.Salary = employee.Salary;
                Employees.Email = employee.Email;
                Employees.PhoneNumber = employee.PhoneNumber;
                Employees.HiringDate = employee.HiringDate;
                Employees.Gender = employee.Gender;
                Employees.EmployeeType = employee.EmployeeType;
                Employees.CreateBy = employee.CreateBy;
                Employees.LastModifiedBy = employee.LastModifiedBy;
            }
        }
    }
}
