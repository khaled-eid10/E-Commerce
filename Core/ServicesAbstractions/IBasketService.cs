using Shared.Basket;

namespace ServicesAbstractions;
public interface IBasketService
{
    Task<BasketDTO> GetAsync(string id);
    Task<BasketDTO> UpdateAsync(BasketDTO basketDTO);
    Task DeleteAsync(string id);

}
