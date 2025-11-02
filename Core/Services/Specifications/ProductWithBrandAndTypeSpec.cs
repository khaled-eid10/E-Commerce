using Domain.Models.Products;

namespace Services.Specifications;
internal class ProductWithBrandAndTypeSpec
    : BaseSpecifications<Product>
{

    // use  to Get Product by id 

    public ProductWithBrandAndTypeSpec(int id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }

    // Get All
    public ProductWithBrandAndTypeSpec(ProductQueryParameters parameters)
       : base(CreateCriteria(parameters))
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
        ApplySorting(parameters.Sort);
        ApplyPagination(parameters.PageSize, parameters.PageIndex);
    }

    private static System.Linq.Expressions.Expression<Func<Product, bool>>
        CreateCriteria(ProductQueryParameters parameters)
    {
        return product => (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
                    (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) &&
                    (string.IsNullOrWhiteSpace(parameters.Search) ||
                    product.Name.ToLower().Contains(parameters.Search.ToLower()));
    }

    private void ApplySorting(ProductSortingOptions options)
    {
        switch (options)
        {
            case ProductSortingOptions.NameAsc:
                AddOrderBy(p => p.Name);
                break;
            case ProductSortingOptions.NameDesc:
                AddOrderByDesc(p => p.Name);
                break;
            case ProductSortingOptions.PriceAsc:
                AddOrderBy(p => p.Price);
                break;
            case ProductSortingOptions.PriceDesc:
                AddOrderByDesc(p => p.Price);
                break;
            default:
                break;
        }
    }
}
//Where( product => (!brandId.HasValue || product.BrandId == brandId.Value) &&
//(!typeId.HasValue || product.TypeId == typeId.Value))