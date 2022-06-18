using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Core.Services
{
    public interface IProductFeatureService : IService<ProductFeature>
    {

        Task<CustomResponseDTO<List<ProductFeatureWithProductDTO>>> GetProductFeaturesWithProductAsync();
        
        Task<CustomResponseDTO<ProductFeatureWithProductDTO>> GetProductFeatureWithProductAsync(int ProductFeatureID);   

    }
}
