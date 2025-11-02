

namespace Services;
internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    : IProductService
{
    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
    {
        var repo = unitOfWork.GetRepository<ProductBrand, int>();

        // TEntity =>  BaseEntity<TKey>
        // Product => BaseEntity<int>
        var data = await repo.GetAllAsync();

        return mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponse>>(data);

    }

    public async Task<ProductResponse> GetProductAsync(int id)
    {
        var repo = unitOfWork.GetRepository<Product, int>();
        var data = await repo.GetAsync(new ProductWithBrandAndTypeSpec(id))
            ?? throw new ProductNotFoundException(id);

        return mapper.Map<Product, ProductResponse>(data);
    }

    public async Task<PaginatedResponse<ProductResponse>> GetProductsAsync(ProductQueryParameters parameters)
    {
        var repo = unitOfWork.GetRepository<Product, int>();
        var data = await repo.GetAllAsync(new ProductWithBrandAndTypeSpec(parameters));

        var productsResponse = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(data);
        var count = productsResponse.Count();
        var totalCount = await repo.CountAsync(new ProductCountSpecifications(parameters));
        return new(parameters.PageIndex, count, totalCount, productsResponse);
    }

    public async Task<IEnumerable<TypeResponse>> GetTypesAsync()
    {
        var repo = unitOfWork.GetRepository<ProductType, int>();

        var data = await repo.GetAllAsync();

        return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(data);
    }
}
