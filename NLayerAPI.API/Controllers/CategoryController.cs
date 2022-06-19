using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerAPI.API.Filters;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
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

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategoriesWithProducts(int categoryId)
        {

            return CreateActionResult(await _categoryService.GetCategoryWithProductsAsync(categoryId));

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCategoriesWithProducts()
        {

            return CreateActionResult(await _categoryService.GetCategoriesWithProductsAsync());

        }

        [HttpGet]
        public async Task<IActionResult> All()
        {

            var categories = await _categoryService.GetAllAsync();

            var categoriesDtos = _mapper.Map<List<CategoryDTO>>(categories.ToList());

            return CreateActionResult(CustomResponseDTO<List<CategoryDTO>>.Success(200, categoriesDtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var category = await _categoryService.GetByIdAsync(id);

            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return CreateActionResult(CustomResponseDTO<CategoryDTO>.Success(200, categoryDto));

        }


        [HttpPost]
        public async Task<IActionResult> Save(CategoryDTO categoryDTO)
        {

            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDTO));

            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return CreateActionResult(CustomResponseDTO<CategoryDTO>.Success(201, categoryDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDTO categoryUpdateDTO)
        {

            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryUpdateDTO));

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            var category = await _categoryService.GetByIdAsync(id);

            await _categoryService.RemoveAsync(category);

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));

        }




    }
}
