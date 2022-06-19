using NLayerAPI.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Core.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

        Task<Category> GetCategoriesWithProductsAsync(int CategoryID);

        Task<List<Category>> GetAllCategoriesWithProductsAsync();   

    }
}
