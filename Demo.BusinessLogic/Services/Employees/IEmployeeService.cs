using Demo.BusinessLogic.DataTransferOpjects.Employee;
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
        IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking);
        EmployeeDetialDto? GetEmployeeById(int id);
        int CreateEmployee(CreateEmployeeDto employeeDto);
        int UpdateEmployee(UpdateEmployeeDto employeeDto);
        bool DeleteEmployee(int id);


    }
}
