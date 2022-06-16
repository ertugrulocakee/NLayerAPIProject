using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Core.Services
{
    public interface ICategoryService : IService<Category>
    {

        Task<CustomResponseDTO<CategoryWithProductsDTO>> GetCategoryWithProductsAsync(int CategoryID);

    }
}
