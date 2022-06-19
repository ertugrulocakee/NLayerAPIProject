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
    public class ProductFeatureServiceWithNoCaching : Service<ProductFeature>, IProductFeatureService
    {

        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IMapper _mapper;
        public ProductFeatureServiceWithNoCaching(IGenericRepository<ProductFeature> repository, IUnitOfWork unitOfWork, IProductFeatureRepository productFeatureRepository, IMapper mapper) : base(repository, unitOfWork)
        {

            _productFeatureRepository = productFeatureRepository;
            _mapper = mapper;
        }

  
        public async Task<CustomResponseDTO<List<ProductFeatureWithProductDTO>>> GetProductFeaturesWithProductAsync()
        {
            
            var productFeatures = await _productFeatureRepository.GetProductFeaturesWithProductAsync();

            var productFeatureDTOs = _mapper.Map<List<ProductFeatureWithProductDTO>>(productFeatures);

            return CustomResponseDTO<List<ProductFeatureWithProductDTO>>.Success(200, productFeatureDTOs);

        }

        public async Task<CustomResponseDTO<ProductFeatureWithProductDTO>> GetProductFeatureWithProductAsync(int ProductFeatureID)
        {

            var productFeature = await _productFeatureRepository.GetProductFeatureWithProductAsync(ProductFeatureID);

            var productFeatureDTO = _mapper.Map<ProductFeatureWithProductDTO>(productFeature);  

            return CustomResponseDTO<ProductFeatureWithProductDTO>.Success(200, productFeatureDTO); 

        }
    }
}
