using Demo.BusinessLogic.DataTransferOpjects;
using Demo.DataAccess.Moodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDbo ToDepartmentDbo(this Department D)
        {
            return new DepartmentDbo()
            {
                DepId = D.Id,
                Code = D.Code,
                Description = D.Description,
                Name = D.Name,
                DateOfCreation = DateOnly.FromDateTime(D.CreateOn)
            };

        }

        public static DepartmentDetialsDto ToDepartmentDetialsDto(this Department D)


        {
            return new DepartmentDetialsDto()
            {
                Id= D.Id,
                Name= D.Name,
                CreateOn = DateOnly.FromDateTime(D.CreateOn),

            };
        }

        public static Department ToEntity(this CreateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                Name = departmentDto.Nmae,
                CreateOn = departmentDto.DateOfCreate.ToDateTime(new TimeOnly()),
            };

        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department()
        {
            Id = departmentDto.Id,
            Name = departmentDto.Nmae,
            Code = departmentDto.Code,
            CreateOn = departmentDto.DateOfCreate.ToDateTime(new TimeOnly()),
            Description = departmentDto.Description,

        };
            
            
        
        
    }
}
