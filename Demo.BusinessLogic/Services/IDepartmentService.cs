using Demo.BusinessLogic.DataTransferOpjects;

namespace Demo.BusinessLogic.Services
{
    public interface IDepartmentService
    {
        int AddDepartment(CreateDepartmentDto departmentDbo);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDbo> GetAllDepartments();
        DepartmentDetialsDto GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}