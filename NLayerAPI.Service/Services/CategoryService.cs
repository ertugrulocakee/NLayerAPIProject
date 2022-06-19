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
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<List<CategoryWithProductsDTO>>> GetCategoriesWithProductsAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesWithProductsAsync();

            var categoryDtos = _mapper.Map<List<CategoryWithProductsDTO>>(categories);

            return CustomResponseDTO<List<CategoryWithProductsDTO>>.Success(200, categoryDtos);

        }

        public async Task<CustomResponseDTO<CategoryWithProductsDTO>> GetCategoryWithProductsAsync(int CategoryID)
        {

            var category = await _categoryRepository.GetCategoriesWithProductsAsync(CategoryID);

            var categoryDto = _mapper.Map<CategoryWithProductsDTO>(category);

            return CustomResponseDTO<CategoryWithProductsDTO>.Success(200, categoryDto);

        }
    }
}
