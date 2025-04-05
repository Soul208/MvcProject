using Demo.BusinessLogic.DataTransferOpjects.DepartmentDto;

namespace Demo.BusinessLogic.Services.DepartmentsServies
{
    public interface IDepartmentService
    {
        int CreateDepartment(CreateDepartmentDto departmentDbo);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDbo> GetAllDepartments();
        DepartmentDetialsDto GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}