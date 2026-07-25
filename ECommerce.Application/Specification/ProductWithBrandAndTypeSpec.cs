using ECommerce.Domin.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specification
{
    public class ProductWithBrandAndTypeSpec : BaseSpecification<Product, int>
    {
        // Get All
        public ProductWithBrandAndTypeSpec()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
