using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenPay.Data.Models;
using OpenPay.Data.Repositories;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;

namespace OpenPay.Data.Configuration;
public static class RegisterServices
{
    public static void AddDataServices(this IServiceCollection services, IConfiguration config)
    {
        //services.Configure<DatabaseOptions>(config.GetSection("ConnectionStrings"));

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            //var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            var databaseOptions = new DatabaseOptions(Environment.GetEnvironmentVariable("DB_HOST"),
                Environment.GetEnvironmentVariable("DB_PORT"),
                Environment.GetEnvironmentVariable("DB_USER"),
                Environment.GetEnvironmentVariable("DB_PASSWORD"),
                Environment.GetEnvironmentVariable("DB_NAME"));
            options.UseNpgsql(databaseOptions.DefaultConnection);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }

    public static void SetupDataServices(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }
    }
}