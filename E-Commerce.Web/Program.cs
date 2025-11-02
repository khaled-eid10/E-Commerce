using E_Commerce.Web.Middlewares;
using Persistence;
using Services;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace E_Commerce.Web;
public class Program
{
    public static async Task Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });

        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddWebApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        //builder.Services.AddIdentity<ApplicationUser, IdentityRole>();
        var app = builder.Build();
        await app.InitializeDataBaseAsync();
        app.UseCustomExceptionHandler();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "My E-Commerce API";
                options.DocExpansion(DocExpansion.None);

                //options.InjectStylesheet();

                options.EnableFilter();
                options.DisplayRequestDuration();
            });
        }
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }


}



