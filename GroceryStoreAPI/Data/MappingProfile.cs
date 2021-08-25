using AutoMapper;
using GroceryStoreAPI.Dto;
using GroceryStoreAPI.Entities.Response;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerResponse>().ReverseMap();
            CreateMap<CustomerCreateRequest, Customer>();
            CreateMap<CustomerUpdateRequest, Customer>();
        }
    }
}