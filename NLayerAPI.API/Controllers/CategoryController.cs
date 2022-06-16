using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerAPI.API.Filters;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.Services;

namespace NLayerAPI.API.Controllers
{
    public class CategoryController : CustomBaseController
    {

        private readonly IMapper _mapper;

        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper,  ICategoryService categoryService)
        {
            _mapper = mapper;

            _categoryService = categoryService;
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategoriesWithProducts(int categoryId)
        {

            return CreateActionResult(await _categoryService.GetCategoryWithProductsAsync(categoryId));

        }

        



    }
}
