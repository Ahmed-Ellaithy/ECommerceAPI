using ECommerce.Application.Common;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Domin.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetPAllBrandsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default);
        Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default);
    }

}
