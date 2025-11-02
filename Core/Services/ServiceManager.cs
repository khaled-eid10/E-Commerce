namespace Services;
public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,
    IBasketRepository basketRepository
    , UserManager<ApplicationUser> userManager
    , IOptions<JWTOptions> options)
//: IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService =
        new(() => new ProductService(unitOfWork, mapper));
    public IProductService ProductService => _lazyProductService.Value;


    private readonly Lazy<IBasketService> _lazyBasketService =
    new(() => new BasketService(basketRepository, mapper));
    public IBasketService BasketService => _lazyBasketService.Value;


    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
   new(() => new AuthenticationService(userManager, mapper, options));
    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;


    private readonly Lazy<IOrderService> _lazyOrderService =
        new(() => new OrderService(mapper, unitOfWork, basketRepository));
    public IOrderService OrderService => _lazyOrderService.Value;
}
