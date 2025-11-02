using Shared.Basket;

namespace ServicesAbstractions;
public interface IPaymentService
{
    Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId);
    Task UpdateOrderPaymentStatusAsync(string jsonRequest, string stripeHeader);
}
