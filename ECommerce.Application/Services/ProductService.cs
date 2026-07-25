using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Application.Specification;
using ECommerce.Domin.Contracts;
using ECommerce.Domin.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<IReadOnlyList<BrandDto>>> GetPAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            // ProductBrand table to Dto
            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(data);

        }


        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var Spec = new ProductWithBrandAndTypeSpec();
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Spec, ct);
            return Result<IReadOnlyList<ProductDto>>.Ok(_mapper.Map<IReadOnlyList<ProductDto>>(products));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types = _mapper.Map<IReadOnlyList<TypeDto>>(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct));
            return Result<IReadOnlyList<TypeDto>>.Ok(types);
        }

        

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id, ct);
            if (product == null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound",$"Product with id {id} not found"));
            return Result<ProductDto>.Ok(_mapper.Map<ProductDto>(product));
        }
    }
}
