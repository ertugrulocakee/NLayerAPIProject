using Microsoft.EntityFrameworkCore;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.Repository;
using NLayerAPI.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Repository.Repository
{
    public class ProductFeatureRepository : GenericRepository<ProductFeature>, IProductFeatureRepository
    {
        public ProductFeatureRepository(AppDBContext context) : base(context)
        {
        }

        public async Task<List<ProductFeature>> GetProductFeaturesWithProductAsync()
        {
            return await _context.ProductFeatures.Include(x => x.Product).ToListAsync();
        }

        public async Task<ProductFeature> GetProductFeatureWithProductAsync(int ProductFeatureID)
        {
            return await _context.ProductFeatures.Include(x => x.Product).Where(x=>x.Id == ProductFeatureID).SingleOrDefaultAsync();
        }
    }
}
