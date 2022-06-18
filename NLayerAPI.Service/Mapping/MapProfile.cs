using AutoMapper;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Service.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {

             CreateMap<Product,ProductDTO>().ReverseMap();  
             CreateMap<Category,CategoryDTO>().ReverseMap();
             CreateMap<ProductFeature, ProductFeatureDTO>().ReverseMap();
             CreateMap<ProductUpdateDTO, Product>();
             CreateMap<Product, ProductWithCategoryDTO>();
             CreateMap<Category, CategoryWithProductsDTO>();
             CreateMap<ProductFeature, ProductFeatureWithProductDTO>();

        }

    }
}
