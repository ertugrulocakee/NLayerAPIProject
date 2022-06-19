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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDBContext context) : base(context)
        {

        }

        public async Task<List<Category>> GetAllCategoriesWithProductsAsync()
        {
            return  await _context.Categories.Include(x => x.Products).ToListAsync();

        }

        public async Task<Category> GetCategoriesWithProductsAsync(int CategoryID)
        {
            return await _context.Categories.Include(x => x.Products).Where(x=>x.Id == CategoryID).SingleOrDefaultAsync();
        }
    }
}
