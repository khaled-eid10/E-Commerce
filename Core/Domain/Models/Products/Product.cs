namespace Domain.Models.Products;
public class Product : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public ProductBrand ProductBrand { get; set; } // Reference Navigational Property
    public int BrandId { get; set; } // FK 
    public ProductType ProductType { get; set; } // Reference Navigational Property
    public int TypeId { get; set; } // FK 
}
