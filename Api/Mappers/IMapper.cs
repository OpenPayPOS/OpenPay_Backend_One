using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Api.Mappers;

public interface IMapper<TOut, TIn>
{
    public abstract Task<TOut> MapDtoToModelAsync(TIn input);
}
