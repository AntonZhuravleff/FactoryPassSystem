using AutoMapper;
using FactoryPassSystem.WebAPI.Entities;
using FactoryPassSystem.WebAPI.Models;

namespace FactoryPassSystem.WebAPI.Mapper
{
    public class AspNetWebApiProfile : Profile
    {
        public AspNetWebApiProfile()
        {
            CreateMap<Employee, CreateEmployeeRequest>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeRequest>().ReverseMap();
            CreateMap<Employee, EmployeeResponse>().ReverseMap();

            CreateMap<Position, PositionResponse>().ReverseMap();

            CreateMap<Shift, ShiftResponse>().ReverseMap();
        }
    }
}
