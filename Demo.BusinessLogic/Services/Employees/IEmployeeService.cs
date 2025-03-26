using Demo.DataAccess.Moodels.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        void AddNewEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
    }
}
