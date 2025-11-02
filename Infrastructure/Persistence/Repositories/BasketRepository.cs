
namespace Persistence.Repositories;
public class BasketRepository(IConnectionMultiplexer connection)
    : IBasketRepository
{

    private readonly IDatabase _database = connection.GetDatabase();
    public async Task DeleteAsync(string id) => await _database.KeyDeleteAsync(id);

    public async Task<CustomerBasket?> GetAsync(string id)
    {
        var basket = await _database.StringGetAsync(id);


        if (basket.IsNullOrEmpty) return null;

        return JsonSerializer.Deserialize<CustomerBasket>(basket!);
    }

    public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan? timeSpan = null)
    {
        var jsonBasket = JsonSerializer.Serialize(basket);
        var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket
                , timeSpan ?? TimeSpan.FromDays(7));

        return isCreatedOrUpdated ? await GetAsync(basket.Id) : null;
    }
}
