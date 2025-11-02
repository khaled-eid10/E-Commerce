
namespace Persistence;
public class DbInitializer(StoreDbContext context,
    StoreIdentityDbContext identityContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
    : IDbInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            /// Production =>  Create Db + Seeding 
            /// Dev => Seeding 
            //if ((await context.Database.GetPendingMigrationsAsync()).Any())
            //    await context.Database.MigrateAsync();

            if (!context.Set<DeliveryMethod>().Any())
            {
                // Read From the file 
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\delivery.json");
                // Convert  to C# objects  [Deserialize] 
                var objects = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);
                // Save to DB 
                if (objects is not null && objects.Any())
                {
                    context.Set<DeliveryMethod>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Set<ProductBrand>().Any())
            {
                // Read From the file 
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                // Convert  to C# objects  [Deserialize] 
                var objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                // Save to DB 
                if (objects is not null && objects.Any())
                {
                    context.Set<ProductBrand>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Set<ProductType>().Any())
            {
                // Read From the file 
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                // Convert  to C# objects  [Deserialize] 
                var objects = JsonSerializer.Deserialize<List<ProductType>>(data);
                // Save to DB 
                if (objects is not null && objects.Any())
                {
                    context.Set<ProductType>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Set<Product>().Any())
            {
                // Read From the file 
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                // Convert  to C# objects  [Deserialize] 
                var objects = JsonSerializer.Deserialize<List<Product>>(data);
                // Save to DB 
                if (objects is not null && objects.Any())
                {
                    context.Set<Product>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
    }

    public async Task InitializeIdentityAsync()
    {
        //if ((await identityContext.Database.GetPendingMigrationsAsync()).Any())
        //    await identityContext.Database.MigrateAsync();
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }

        if (!userManager.Users.Any())
        {
            var superAdminUser = new ApplicationUser
            {
                DisplayName = "Super Admin",
                Email = "SuperAdmin@gmail.com",
                UserName = "SuperAdmin",
                PhoneNumber = "0123465789"
            };

            var adminUser = new ApplicationUser
            {
                DisplayName = "Admin",
                Email = "Admin@gmail.com",
                UserName = "Admin",
                PhoneNumber = "0123465789"
            };

            await userManager.CreateAsync(superAdminUser, "Passw0rd");
            await userManager.CreateAsync(adminUser, "Passw0rd");


            await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
