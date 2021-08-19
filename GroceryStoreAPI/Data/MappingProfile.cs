using AutoMapper;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerCreateDto>().ReverseMap();
        }
    }
}