using AutoMapper;
using CustomerApi.Application.DTOs;
using CustomerApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Customer,CustomerEditDto>().ReverseMap();
        }
    }
}
