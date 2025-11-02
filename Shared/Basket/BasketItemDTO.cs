namespace Shared.Basket;
public record BasketItemDTO
{
    public int Id { get; init; }

    public string ProductName { get; init; }
    public string PictureUrl { get; init; }
    [Range(1, short.MaxValue)]
    public decimal Price { get; init; }
    [Range(1, short.MaxValue)]
    public int Quantity { get; init; }
}
