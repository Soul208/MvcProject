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
    public class DepartmentService(IunitOfWork _unitOfWork) : IDepartmentService
    {
        public IEnumerable<DepartmentDbo> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDbo());
        }

        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            //return department is null ? null : department.ToDepartmentDetialsDto();
            return department?.ToDepartmentDetialsDto();
        }

        public int CreateDepartment(CreateDepartmentDto departmentDbo)
        {
            var department = departmentDbo.ToEntity();
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
             _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(Department);
                int result = _unitOfWork.SaveChanges();
                if (result > 0) return true;
                else return false;
            }

        }
    }
}
