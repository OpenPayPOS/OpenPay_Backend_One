using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.EntityFrameworkCore;
using OpenPay.Data.DataModels;
using OpenPay.Interfaces.Data.DataModels;
using OpenPay.Interfaces.Data.Repositories.Common;

namespace OpenPay.Data.Repositories;

public abstract class BaseRepository<TModel, TDTO> : IBaseRepository<TDTO> where TModel : BaseDataModel
{
    protected readonly AppDbContext _dbContext;
    private readonly ILogger<BaseRepository<TModel, TDTO>> _logger;

    protected BaseRepository(AppDbContext dbContext, ILogger<BaseRepository<TModel, TDTO>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IAsyncEnumerable<TDTO> GetAllAsync()
    {
        return _dbContext.Set<TModel>().Select(i =>
            MapToDataDTO(i)).AsAsyncEnumerable();
    }

    public async Task<Optional<TDTO>> GetByIdAsync(Guid id)
    {
        try
        {
            TModel? model = await _dbContext.Set<TModel>().FindAsync(id);
            if (model == null) return new NotFoundException("Item with that Id could not be found");

            return MapToDataDTO(model);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);
            return ex;
        }
    }

    public async Task<Optional> DeleteAsync(Guid id)
    {
        try
        {
            var set = _dbContext.Set<TModel>();
            TModel? model = await set.FindAsync(id);
            if (model == null) return new NotFoundException("Item with that Id could not be found");

            set.Remove(model);
            return new();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);

            return ex;
        }
    }

    public async Task<Optional<bool>> IdExistsAsync(Guid id)
    {
        try
        {
            return await _dbContext.Set<TModel>().CountAsync(x => x.Id == id) > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);
            return ex;
        }
    }

    protected abstract TDTO MapToDataDTO(TModel model);
}
