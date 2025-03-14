using Microsoft.Extensions.DependencyInjection;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.Internal;

namespace OpenPay.Services.Configuration;
public static class RegisterServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IInternalItemService, ItemService>();
        services.AddScoped<IOrderService, OrderService>();
    }
}