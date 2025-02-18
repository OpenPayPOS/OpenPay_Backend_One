using Microsoft.Extensions.DependencyInjection;
using OpenPay.Interfaces.Services;

namespace OpenPay.Services.Configuration;
public static class RegisterServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IItemService, ItemService>();
    }
}