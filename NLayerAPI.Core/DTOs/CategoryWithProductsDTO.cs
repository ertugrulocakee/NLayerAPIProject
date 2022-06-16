using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Core.DTOs
{
    public class CategoryWithProductsDTO : CategoryDTO
    {

        public List<ProductDTO> Products { get; set; }    

    }
}
