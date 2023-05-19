using AutoMapper;
using Charisma.Application.ProductApplication.Models;
using Charisma.Domain;

namespace Charisma.Application.ProductApplication.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}