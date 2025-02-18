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
        services.Configure<DatabaseOptions>(config.GetSection("ConnectionStrings"));

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseNpgsql(databaseOptions.DefaultConnection);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IItemRepository, ItemRepository>();
    }
}