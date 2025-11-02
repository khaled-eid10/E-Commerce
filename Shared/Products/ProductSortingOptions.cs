using System.Text.Json.Serialization;

namespace Shared.Products;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductSortingOptions
{
    NameAsc,
    NameDesc,
    PriceAsc,
    PriceDesc
}
