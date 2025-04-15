using AutoMapper;
using Demo.BusinessLogic.DataTransferOpjects.Employee;
using Demo.DataAccess.Moodels.EmployeeModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.EmpGender , Options => Options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmpType , Options => Options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Department , Options => Options.MapFrom(src => src.Department !=null ? src.Department.Name : null));

            CreateMap<Employee, EmployeeDetialDto>()
                .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.Image , Options => Options.MapFrom(src => src.ImageName)); ;

            CreateMap<CreateEmployeeDto, Employee>()
                 .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdateEmployeeDto, Employee>()
                 .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));


        }
    }
}
