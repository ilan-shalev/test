using AutoMapper;
using Test.Data.Entities;
using Test.Dtos;

namespace Test.Profiles
{

    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<ProductDeleteDto, Product>();
        }
    }
}
