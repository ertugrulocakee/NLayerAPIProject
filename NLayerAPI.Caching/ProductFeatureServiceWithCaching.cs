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
    public class ProductFeatureServiceWithCaching : IProductFeatureService
    {

        private const string CacheProductFeatureKey = "productFeaturesCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductFeatureRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductFeatureServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductFeatureRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;



            if (!_memoryCache.TryGetValue(CacheProductFeatureKey, out _))
            {

                _memoryCache.Set(CacheProductFeatureKey, _repository.GetProductFeaturesWithProductAsync().Result);

            }

        }

        public async Task<ProductFeature> AddAsync(ProductFeature entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductFeaturesAsync();
            return entity;
        }

        public async Task<IEnumerable<ProductFeature>> AddRangeAsync(IEnumerable<ProductFeature> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductFeaturesAsync();

            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<ProductFeature, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductFeature>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<ProductFeature>>(CacheProductFeatureKey));
        }

        public Task<ProductFeature> GetByIdAsync(int id)
        {

            var productFeature = _memoryCache.Get<List<ProductFeature>>(CacheProductFeatureKey).FirstOrDefault(x => x.Id == id);

            if (productFeature == null)
            {

                throw new NotFoundException($"{typeof(ProductFeature).Name}({id}) not found!");
            }


            return Task.FromResult(productFeature);

        }

        public Task<CustomResponseDTO<List<ProductFeatureWithProductDTO>>> GetProductFeaturesWithProductAsync()
        {
            var productFeatures = _memoryCache.Get<IEnumerable<ProductFeature>>(CacheProductFeatureKey);

            var productFeaturesWithProductDTOs = _mapper.Map<List<ProductFeatureWithProductDTO>>(productFeatures);

            return Task.FromResult(CustomResponseDTO<List<ProductFeatureWithProductDTO>>.Success(200, productFeaturesWithProductDTOs));

        }

        public Task<CustomResponseDTO<ProductFeatureWithProductDTO>> GetProductFeatureWithProductAsync(int ProductFeatureID)
        {
            var productFeature = _memoryCache.Get<IEnumerable<ProductFeature>>(CacheProductFeatureKey).FirstOrDefault(x => x.Id == ProductFeatureID);

            var productFeatureWithProductDTO = _mapper.Map<ProductFeatureWithProductDTO>(productFeature);

            return Task.FromResult(CustomResponseDTO<ProductFeatureWithProductDTO>.Success(200, productFeatureWithProductDTO));

        }

        public async Task RemoveAsync(ProductFeature entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductFeaturesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<ProductFeature> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductFeaturesAsync();
        }

        public async Task UpdateAsync(ProductFeature entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductFeaturesAsync();
        }

        public IQueryable<ProductFeature> Where(Expression<Func<ProductFeature, bool>> expression)
        {
            return _memoryCache.Get<List<ProductFeature>>(CacheProductFeatureKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllProductFeaturesAsync()
        {

            _memoryCache.Set(CacheProductFeatureKey, await _repository.GetProductFeaturesWithProductAsync());

        }
    }
}
