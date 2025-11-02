using Shared.Orders;

namespace ServicesAbstractions;
public interface IOrderService
{
    // Create ()

    Task<OrderResponse> CreateAsync(OrderRequest request, string email);
    Task<OrderResponse> GetAsync(Guid id);
    Task<IEnumerable<OrderResponse>> GetAllAsync(string email);
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync();
}
