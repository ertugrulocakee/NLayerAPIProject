using NLayerAPI.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Core.Repository
{
    public interface IProductFeatureRepository : IGenericRepository<ProductFeature>
    {

        Task<List<ProductFeature>> GetProductFeaturesWithProductAsync();

        Task<ProductFeature> GetProductFeatureWithProductAsync(int ProductFeatureID);

    }
}
