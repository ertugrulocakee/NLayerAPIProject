using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ProductServiceWithCaching : IProductService
    {

        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

    
        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;


            if(!_memoryCache.TryGetValue(CacheProductKey, out _))
            {

                _memoryCache.Set(CacheProductKey,_repository.GetProductsWithCategory().Result);

            }


        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;  
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

            return entities;
            
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {

            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));

        }

        public Task<Product> GetByIdAsync(int id)
        {

            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if(product == null)
            {

                throw new NotFoundException($"{typeof(Product).Name}({id}) not found!");
            }


            return Task.FromResult(product);

        }

        public Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategories()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);

            var productWithCategoryDTOs = _mapper.Map<List<ProductWithCategoryDTO>>(products);

            return Task.FromResult(CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(200, productWithCategoryDTOs));
            
        }

        public async Task RemoveAsync(Product entity)
        {

            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {

            _repository.RemoveRange(entities);  
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity); 
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }


        public  async Task CacheAllProductsAsync()
        {

            _memoryCache.Set(CacheProductKey,  await _repository.GetProductsWithCategory());

        }

        public Task<CustomResponseDTO<ProductWithCategoryDTO>> GetProductWithCategory(int ProductID)
        {

            var product = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == ProductID);

            var productWithCategoryDTO = _mapper.Map<ProductWithCategoryDTO>(product);

            return Task.FromResult(CustomResponseDTO<ProductWithCategoryDTO>.Success(200, productWithCategoryDTO));

        }
    }
}
