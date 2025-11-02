using Shared.Basket;

namespace Presentation.Controllers;
public class BasketsController(IServiceManager serviceManager)
    : APIController
{
    // Update => Create Basket , Add Item To Basket , Remove Item From Basket  , + - Quantity for Item in Basket
    [HttpPost]
    public async Task<ActionResult<BasketDTO>> Update(BasketDTO basket)
        => Ok(await serviceManager.BasketService.UpdateAsync(basket));
    // Delete Basket => when Create order is done 
    [HttpDelete]
    public async Task<ActionResult<BasketDTO>> Delete(string id)
    {
        await serviceManager.BasketService.DeleteAsync(id);
        return NoContent(); // 204 without body 
    }
    // Get Basket By Id 
    [HttpGet]
    public async Task<ActionResult<BasketDTO>> Get(string id)
        => Ok(await serviceManager.BasketService.GetAsync(id));
}
