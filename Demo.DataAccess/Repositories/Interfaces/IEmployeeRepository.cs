using Demo.DataAccess.Moodels.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        int Add(Employee Employee);
        IEnumerable<Employee> GetAll(bool WithTracking = false);
        Employee? GetById(int id);
        int Remove(Employee Employee);
        int Update(Employee Employee);
    }
}
