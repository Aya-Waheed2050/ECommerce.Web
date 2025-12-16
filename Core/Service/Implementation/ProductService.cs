using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.NotFoundExceptions;
using Domain.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject.ProductDtos;

namespace Service.Implementation
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    { 

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters Parameters)
        { 
            IGenericRepository<Product, int>? Repo = _unitOfWork.GetRepository<Product, int>();

            var Specifications = new ProductWithBrandAndTypeSpecifications(Parameters);
            IEnumerable<Product>? Products = await Repo.GetAllAsync(Specifications);
            IEnumerable<ProductResultDto> productResult = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResultDto>>(Products);
            //int PageSize = productResult.Count();
            int PageSize = Parameters.PageSize;
            int TotalCount = await Repo.CountAsync(new ProductCountSpecifications(Parameters));
            return new PaginatedResult<ProductResultDto>(Parameters.PageIndex, PageSize, TotalCount, productResult);
        }
        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            Product? Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            return (Product is null) ? throw new ProductNotFoundException(id) :
                                      _mapper.Map<Product, ProductResultDto>(Product);
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            IEnumerable<ProductBrand>? Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResultDto>>(Brands); ;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            IEnumerable<ProductType>? Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResultDto>>(Types);
        }


    }
}
