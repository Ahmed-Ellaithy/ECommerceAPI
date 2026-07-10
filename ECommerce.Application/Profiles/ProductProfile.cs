using AutoMapper;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Domin.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<ProductBrand,BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name));
        }
    }
}
