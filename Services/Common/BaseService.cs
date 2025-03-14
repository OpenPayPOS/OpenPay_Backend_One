using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.Extensions.Logging;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories.Common;
using OpenPay.Services.Models;

namespace OpenPay.Services.Common;

// TODO: rewrite tests for services to seperate base service
public abstract class BaseService<TModel, TDTO, TDataDTO>
        where TDTO : struct
        where TDataDTO : struct
        where TModel : BaseModel<TModel, TDTO, TDataDTO>
{
    private readonly IBaseRepository<TDataDTO> _repository;
    private readonly ILogger<BaseService<TModel, TDTO, TDataDTO>> _logger;
    protected readonly IUnitOfWork _unitOfWork;

    protected BaseService(IBaseRepository<TDataDTO> repository,
        ILogger<BaseService<TModel, TDTO, TDataDTO>> logger,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Optional<TDTO>> GetByIdAsync(Guid id)
    {
        var existsOptional = await _repository.IdExistsAsync(id);

        if (existsOptional.IsInvalid) return (Exception)existsOptional;

        if (existsOptional.Handle(exists => !exists, _ => false))
        {
            return new NotFoundException("Item with that Guid could not be found.");
        }

        var itemData = await _repository.GetByIdAsync(id);

        return await itemData.HandleAsync(async data =>
        {
            TModel model = FromDataDTO(data);
            return new Optional<TDTO>(await model.ToDTOAsync());
        }, ex => ex);
    }
    public async Task<Optional> DeleteAsync(Guid id)
    {
        var existsOptional = await _repository.IdExistsAsync(id);

        if (existsOptional.IsInvalid) return new(existsOptional);

        if (existsOptional.Handle(exists => !exists, _ => false))
        {
            return new(new NotFoundException("Item with that Guid could not be found."));
        }

        var data = await _repository.DeleteAsync(id);

        return await data.HandleAsync(async _ =>
        {
            await _unitOfWork.SaveChangesAsync();
            return new Optional();
        }, ex => new(ex));
    }

    public Task<Optional<bool>> IdExistsAsync(Guid id)
    {
        return _repository.IdExistsAsync(id);
    }

    public abstract TModel FromDataDTO(TDataDTO dataDTO);
}