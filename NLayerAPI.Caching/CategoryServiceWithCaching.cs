using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using NLayerAPI.Core.Repository;
using NLayerAPI.Core.Services;
using NLayerAPI.Core.UnitOfWorks;
using NLayerAPI.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Caching
{
    public class CategoryServiceWithCaching : ICategoryService
    {

        private const string CacheCategoryKey = "categoriesCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;



            if (!_memoryCache.TryGetValue(CacheCategoryKey, out _))
            {

                _memoryCache.Set(CacheCategoryKey, _repository.GetAllCategoriesWithProductsAsync().Result);

            }


        }

        public async Task<Category> AddAsync(Category entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoriesAsync();
            return entity;
        }

        public async Task<IEnumerable<Category>> AddRangeAsync(IEnumerable<Category> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoriesAsync();

            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey));

        }

        public Task<Category> GetByIdAsync(int id)
        {
            var category = _memoryCache.Get<List<Category>>(CacheCategoryKey).FirstOrDefault(x => x.Id == id);

            if (category == null)
            {

                throw new NotFoundException($"{typeof(Category).Name}({id}) not found!");
            }


            return Task.FromResult(category);
        }

        public Task<CustomResponseDTO<List<CategoryWithProductsDTO>>> GetCategoriesWithProductsAsync()
        {
            var categories = _memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey);

            var categoryWithProductDTOs = _mapper.Map<List<CategoryWithProductsDTO>>(categories);

            return Task.FromResult(CustomResponseDTO<List<CategoryWithProductsDTO>>.Success(200, categoryWithProductDTOs));

        }

        public Task<CustomResponseDTO<CategoryWithProductsDTO>> GetCategoryWithProductsAsync(int CategoryID)
        {

            var category = _memoryCache.Get<IEnumerable<Category>>(CacheCategoryKey).FirstOrDefault(x => x.Id == CategoryID);

            var categoryWithProductDTO = _mapper.Map<CategoryWithProductsDTO>(category);

            return Task.FromResult(CustomResponseDTO<CategoryWithProductsDTO>.Success(200, categoryWithProductDTO));

        }

        public async Task RemoveAsync(Category entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoriesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Category> entities)
        {

            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoriesAsync();

        }

        public async Task UpdateAsync(Category entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllCategoriesAsync();
        }

        public IQueryable<Category> Where(Expression<Func<Category, bool>> expression)
        {
            return _memoryCache.Get<List<Category>>(CacheCategoryKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllCategoriesAsync()
        {

            _memoryCache.Set(CacheCategoryKey, await _repository.GetAllCategoriesWithProductsAsync());

        }
    }
}
