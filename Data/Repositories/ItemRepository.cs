using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenPay.Data.DataModels;
using OpenPay.Data.Mappers;
using OpenPay.Interfaces.Data.DataModels.Item;
using OpenPay.Interfaces.Data.Repositories;

namespace OpenPay.Data.Repositories;
public class ItemRepository : BaseRepository<ItemDataModel, ItemDataDTO>, IItemRepository
{
    private readonly ILogger<ItemRepository> _logger;

    public ItemRepository(AppDbContext dbContext, ILogger<ItemRepository> logger)
        : base(dbContext.Items, logger, new ItemMapper())
    {
        _logger = logger;
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
            await _set.AddAsync(itemDataModel);
            return _mapper.MapToDataDTO(itemDataModel);

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
            ItemDataModel? itemDataModel = await _set.FindAsync(id);
            if (itemDataModel == null) return new NotFoundException("Item with that Id could not be found");

            if (editItemDataDTO.Name != null) itemDataModel.Name = editItemDataDTO.Name;
            if (editItemDataDTO.Price != null) itemDataModel.Price = (decimal)editItemDataDTO.Price;
            if (editItemDataDTO.TaxPercentage != null) itemDataModel.TaxPercentage = (decimal)editItemDataDTO.TaxPercentage;

            return _mapper.MapToDataDTO(itemDataModel);
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
            return await _set.CountAsync(x => x.Name == name) > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred: {error}", ex.Message);
            return ex;
        }
    }
}