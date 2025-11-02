namespace Domain.Models.Basket;
public class CustomerBasket
{
    public string Id { get; set; } // GUID , Generated From Client 
    public IEnumerable<BasketItem> Items { get; set; }

    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
}
