using AutoMapper;
using CS.API.ViewModels;
using CS.Core.Entities;

namespace CS.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CheckOut, CheckOutDto>();
            CreateMap<CheckOutDto, CheckOut>();
            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();
        }
    }
}
