using Demo.BusinessLogic.DataTransferOpjects.DepartmentDto;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Moodels;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.DepartmentsServies
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        public IEnumerable<DepartmentDbo> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDbo());
        }

        public DepartmentDetialsDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentDetialsDto();
        }

        public int CreateDepartment(CreateDepartmentDto departmentDbo)
        {
            var department = departmentDbo.ToEntity();
            return _departmentRepository.Add(department);
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                int result = _departmentRepository.Remove(Department);
                if (result > 0) return true;
                else return false;
            }

        }
    }
}
