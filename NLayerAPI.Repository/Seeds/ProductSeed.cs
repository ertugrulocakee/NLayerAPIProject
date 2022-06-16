using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerAPI.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerAPI.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product { Id = 1, CategoryId = 1, Name = "Faber-Castel GRIP 0.7", Price = 25, Stock = 100 , CreatedDate = DateTime.Now}, new Product { Id = 2, CategoryId = 1, Name = "Faber-Castel GRIP 0.5", Price = 20, Stock = 100, CreatedDate = DateTime.Now }, new Product { Id = 3, CategoryId = 1, Name = "Faber-Castel ECON 0.7", Price = 15, Stock = 100, CreatedDate = DateTime.Now },new Product { Id = 4, CategoryId = 2, Name = "Harry Potter Felsefe Taşı", Price = 40, Stock = 200, CreatedDate = DateTime.Now });
        }
    }
}
