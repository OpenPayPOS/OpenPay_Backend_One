using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenPay.Data.DataModels;
using OpenPay.Interfaces.Data.DataModels;
using OpenPay.Interfaces.Data.Repositories;

namespace OpenPay.Data.Repositories;
public class ItemRepository : IItemRepository
{
    private AppDbContext _dbContext;
    private ILogger<ItemRepository> _logger;

    public ItemRepository(AppDbContext dbContext, ILogger<ItemRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IAsyncEnumerable<ItemDataDTO> GetAllAsync()
    {
        return _dbContext.Items.Select(i =>
            MapToDataDTO(i)).AsAsyncEnumerable();
    }

    public async Task<Optional<ItemDataDTO>> GetByIdAsync(Guid id)
    {
        try
        {
            ItemDataModel? itemDataModel = await _dbContext.Items.FindAsync(id);
            if (itemDataModel == null) return new NotFoundException("Item with that Id could not be found");

            return MapToDataDTO(itemDataModel);
        } catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);
            return ex;
        }
    }


    public async Task<Optional<ItemDataDTO>> CreateAsync(ItemDataDTO itemDataDTO)
    {
        ItemDataModel itemDataModel = new ItemDataModel
        {
            Id = itemDataDTO.Id,
            Name = itemDataDTO.Name,
            Price = itemDataDTO.Price,
            TaxPercentage = itemDataDTO.TaxPercentage,
        };

        try
        {
            await _dbContext.Items.AddAsync(itemDataModel);
            return MapToDataDTO(itemDataModel);

        } catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);

            return ex;
        }
    }

    public async Task<Optional<ItemDataDTO>> EditAsync(Guid id, EditItemDataDTO editItemDataDTO)
    {
        try
        {
            ItemDataModel? itemDataModel = await _dbContext.Items.FindAsync(id);
            if (itemDataModel == null) return new NotFoundException("Item with that Id could not be found");

            if (editItemDataDTO.Name != null) itemDataModel.Name = editItemDataDTO.Name;
            if (editItemDataDTO.Price != null) itemDataModel.Price = (decimal)editItemDataDTO.Price;
            if (editItemDataDTO.TaxPercentage != null) itemDataModel.TaxPercentage = (decimal)editItemDataDTO.TaxPercentage;

            return MapToDataDTO(itemDataModel);
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
            ItemDataModel? itemDataModel = await _dbContext.Items.FindAsync(id);
            if (itemDataModel == null) return new NotFoundException("Item with that Id could not be found");

            _dbContext.Items.Remove(itemDataModel);
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
            return await _dbContext.Items.CountAsync(x => x.Id == id) > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);
            return ex;
        }
    }

    public async Task<Optional<bool>> NameExistsAsync(string name)
    {
        try
        {
            return await _dbContext.Items.CountAsync(x => x.Name == name) > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);
            return ex;
        }
    }

    private static ItemDataDTO MapToDataDTO(ItemDataModel itemDataModel)
    {
        return new ItemDataDTO
        {
            Id = itemDataModel.Id,
            Name = itemDataModel.Name,
            Price = itemDataModel.Price,
            TaxPercentage = itemDataModel.TaxPercentage,
        };
    }
}