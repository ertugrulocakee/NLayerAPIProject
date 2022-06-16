using AutoMapper;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using NLayerAPI.Core.Repository;
using NLayerAPI.Core.Services;
using NLayerAPI.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper,IGenericRepository<Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _repository = productRepository;
            _mapper = mapper;
        }

     

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategories()
        {

            var products= await _repository.GetProductsWithCategory();

            var productsDTO = _mapper.Map<List<ProductWithCategoryDTO>>(products);

            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(200, productsDTO);

        }
    }
}
