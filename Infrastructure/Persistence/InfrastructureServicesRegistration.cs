

namespace Persistence;
public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });
        services.AddDbContext<StoreIdentityDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration.GetConnectionString("RedisConnection");
            return ConnectionMultiplexer.Connect(connectionString!);
        });
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICashRepository, CashRepository>();


        services.AddIdentityCore<ApplicationUser>(config =>
        {
            config.User.RequireUniqueEmail = true;
            //config.User.AllowedUserNameCharacters = 
            //config.Password.RequiredUniqueChars = 2;
            config.Password.RequiredLength = 8;
            config.Password.RequireUppercase = false;
            config.Password.RequireLowercase = false;
            config.Password.RequireDigit = false;
            config.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

        return services;
    }
}
