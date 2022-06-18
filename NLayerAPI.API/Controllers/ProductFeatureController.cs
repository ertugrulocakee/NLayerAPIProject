using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
