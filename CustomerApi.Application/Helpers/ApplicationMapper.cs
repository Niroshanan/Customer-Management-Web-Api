using AutoMapper;
using CustomerApi.Application.DTOs;
using CustomerApi.Data.Entities;

namespace CustomerApi.Application.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Customer,CustomerEditDto>().ReverseMap();
            CreateMap<Customer, CustomerDetailDto>().ReverseMap();
        }
    }
}
