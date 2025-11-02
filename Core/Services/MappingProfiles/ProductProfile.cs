using Microsoft.Extensions.Configuration;
namespace Services.MappingProfiles;
internal class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductBrand, BrandResponse>();
        CreateMap<ProductType, TypeResponse>();

        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ProductBrand,
            options => options.MapFrom(s => s.ProductBrand.Name))
            .ForMember(dest => dest.ProductType,
            options => options.MapFrom(s => s.ProductType.Name))
            .ForMember(dest => dest.PictureUrl,
            options => options.MapFrom<ProductPictureUrlResolver>());

    }
}
internal class ProductPictureUrlResolver(IConfiguration configuration)
    : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context) => string.IsNullOrWhiteSpace(source.PictureUrl) ? string.Empty
        : $"{configuration["BaseUrl"]}{source.PictureUrl}";
}
