using Demo.DataAccess.Moodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferOpjects.DepartmentDto
{
    public class DepartmentDetialsDto
    {
        //public DepartmentDetialsDto(Department department)
        //{
        //    Id = department.Id;
        //    Name = department.Name;
        //    CreateOn = DateOnly.FromDateTime(department.CreateOn);
        //}
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly DateOfCreation { get; set; }
        public int LastModifiedBy { get; set; }
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
