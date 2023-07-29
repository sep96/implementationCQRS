using AutoMapper;
using implementationCQRS.Command;
using implementationCQRS.Dtos;
using implementationCQRS.Models;

namespace implementationCQRS.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
             CreateMap<CreateEmployeeCommand, Employee>()
            .ForMember(c => c.RegistrationDate, opt =>
                opt.MapFrom(_ => DateTime.Now));

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(cd => cd.RegistrationDate, opt =>
                    opt.MapFrom(c => c.RegistrationDate.ToShortDateString()));
        }
    }
}
