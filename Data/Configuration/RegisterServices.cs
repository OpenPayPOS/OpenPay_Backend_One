using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenPay.Data.Repositories;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;

namespace OpenPay.Data.Configuration;
public static class RegisterServices
{
    public static void AddDataServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IItemRepository, ItemRepository>();
    }
}