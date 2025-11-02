using Presentation.Attributes;
namespace Presentation.Controllers;

public class ProductsController(IServiceManager serviceManager)
    : APIController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> Get(int id)
    {
        var data = await serviceManager.ProductService.GetProductAsync(id);
        return Ok(data);
    }

    [RedisCash]
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ProductResponse>>>
        GetProducts([FromQuery] ProductQueryParameters parameters)
    {
        var data = await serviceManager.ProductService.GetProductsAsync(parameters);
        return Ok(data);
    }
    //[Authorize(Roles = "Admin")]
    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands()
    {
        //var email = User.FindFirstValue(ClaimTypes.Email);
        //var userName = User.FindFirstValue(ClaimTypes.Name);
        //var role = User.FindFirstValue(ClaimTypes.Role);

        var data = await serviceManager.ProductService.GetBrandsAsync();
        return Ok(data);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes()
    {
        var data = await serviceManager.ProductService.GetTypesAsync();
        return Ok(data);
    }

}
