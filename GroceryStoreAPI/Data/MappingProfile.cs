using AutoMapper;
using GroceryStoreAPI.Data.Models;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Requests;

namespace GroceryStoreAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerCreateRequest, Customer>();
            CreateMap<CustomerUpdateRequest, Customer>();
        }
    }
}