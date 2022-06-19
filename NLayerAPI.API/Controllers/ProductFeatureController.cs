using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerAPI.API.Filters;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using NLayerAPI.Core.Services;

namespace NLayerAPI.API.Controllers
{
 
    public class ProductFeatureController : CustomBaseController
    {

        private readonly IMapper _mapper;

        private readonly IProductFeatureService _productFeatureService;

        public ProductFeatureController(IMapper mapper, IProductFeatureService productFeatureService)
        {
            _mapper = mapper;

            _productFeatureService = productFeatureService;
        }


        [HttpGet("[action]/{ProductFeatureID}")]
        public async Task<IActionResult> GetProductFeatureWithProduct(int ProductFeatureID)
        {

            return CreateActionResult(await _productFeatureService.GetProductFeatureWithProductAsync(ProductFeatureID));    

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductFeaturesWithProduct()
        {

            return CreateActionResult(await _productFeatureService.GetProductFeaturesWithProductAsync());

        }


        [HttpGet]
        public async Task<IActionResult> All()
        {

            var productFeatures = await _productFeatureService.GetAllAsync();

            var productFeatureDTOs = _mapper.Map<List<ProductFeatureDTO>>(productFeatures.ToList());

            return CreateActionResult(CustomResponseDTO<List<ProductFeatureDTO>>.Success(200, productFeatureDTOs));

        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var productFeature = await _productFeatureService.GetByIdAsync(id);

            var productFeatureDto = _mapper.Map<ProductFeatureDTO>(productFeature);

            return CreateActionResult(CustomResponseDTO<ProductFeatureDTO>.Success(200, productFeatureDto));

        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductFeatureDTO productFeatureDTO)
        {

            var productFeature = await _productFeatureService.AddAsync(_mapper.Map<ProductFeature>(productFeatureDTO));

            var productFeatureDto = _mapper.Map<ProductFeatureDTO>(productFeature);

            return CreateActionResult(CustomResponseDTO<ProductFeatureDTO>.Success(201, productFeatureDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductFeatureDTO productFeatureDTO)
        {

            await _productFeatureService.UpdateAsync(_mapper.Map<ProductFeature>(productFeatureDTO));

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            var productFeature = await _productFeatureService.GetByIdAsync(id);

            await _productFeatureService.RemoveAsync(productFeature);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }



    }
}
