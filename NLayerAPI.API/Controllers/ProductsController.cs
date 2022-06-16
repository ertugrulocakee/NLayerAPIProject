using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerAPI.API.Filters;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using NLayerAPI.Core.Services;
using NLayerAPI.Service.Services;

namespace NLayerAPI.API.Controllers
{
    public class ProductsController : CustomBaseController
    {

        private readonly IMapper _mapper;

        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;   
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {

            return CreateActionResult(await _service.GetProductsWithCategories());

        }


        [HttpGet]   
        public async Task<IActionResult> All()
        {

            var products = await _service.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDTO>>(products.ToList());
        
            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(200, productsDtos));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var product = await _service.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(200, productDto));

        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {

            var product = await _service.AddAsync(_mapper.Map<Product>(productDTO));

            var productsDto = _mapper.Map<List<ProductDTO>>(product);

            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(201, productsDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productUpdateDTO)
        {

            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDTO));

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            var product = await _service.GetByIdAsync(id);


            //if(product == null)
            //{
            //    return CreateActionResult(CustomResponseDTO<NoContentDTO>.Fail(404,"Böyle bir ürün bulunamadı!"));

            //}

            await _service.RemoveAsync(product);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }

    }
}
