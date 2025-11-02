using Shared;
using Shared.Products;

namespace ServicesAbstractions;
public interface IProductService
{
    Task<PaginatedResponse<ProductResponse>> GetProductsAsync(ProductQueryParameters parameters);
    Task<IEnumerable<BrandResponse>> GetBrandsAsync();
    Task<IEnumerable<TypeResponse>> GetTypesAsync();
    Task<ProductResponse> GetProductAsync(int id);
}
