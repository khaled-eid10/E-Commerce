
namespace Services;
internal class BasketService(IBasketRepository basketRepository, IMapper mapper)
    : IBasketService
{
    public async Task DeleteAsync(string id) => await basketRepository.DeleteAsync(id);

    public async Task<BasketDTO> GetAsync(string id)
    {
        // Get Basket 
        // Mapping <Domain , DTO>
        // => Basket DTO 
        var basket = await basketRepository.GetAsync(id) ??
            throw new BasketNotFoundException(id);

        return mapper.Map<CustomerBasket, BasketDTO>(basket);

    }

    public async Task<BasketDTO> UpdateAsync(BasketDTO basketDTO)
    {
        var basket = mapper.Map<CustomerBasket>(basketDTO);
        var updatedBasket = await basketRepository.UpdateAsync(basket) ??
            throw new Exception("Can't Update Basket Now "); // Status 500 

        return mapper.Map<BasketDTO>(updatedBasket);
    }
}
