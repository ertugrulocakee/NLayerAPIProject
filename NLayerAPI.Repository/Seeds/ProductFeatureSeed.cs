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
    internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(new ProductFeature { Id = 1, Color = "Mavi", Height = 200, Width = 100 ,ProductId=1}, new ProductFeature { Id = 2, Color = "Yeşil", Height = 200, Width = 100, ProductId = 2 },new ProductFeature { Id = 3, Color = "Kırmızı", Height = 200, Width = 100, ProductId = 3 }, new ProductFeature { Id = 4,Color="Yeşil-Sarı", Height = 300, Width = 200, ProductId = 4 });
        }
    }
}
