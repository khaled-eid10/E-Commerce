using Domain.Models.Basket;

namespace Domain.Contracts;
public interface IBasketRepository
{
    Task<CustomerBasket?> GetAsync(string id);
    Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan? timeSpan = null); // Update & Create  , TTL 
    Task DeleteAsync(string id);
}
