using Api.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Status, StatusViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Email, EmailViewModel>().ReverseMap();
            CreateMap<Phone, PhoneViewModel>().ReverseMap();
            CreateMap<JobRole, JobRoleViewModel>().ReverseMap();


            CreateMap<AddressViewModel, Address>();
            CreateMap<Address, AddressViewModel>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(x=>x.City.Name))
                .ForMember(dest => dest.State, opt => opt.MapFrom(x => x.City.State.Name));
        }
    }
}
