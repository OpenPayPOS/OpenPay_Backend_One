using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.Extensions.Logging;
using OpenPay.Interfaces.Data.DataModels;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Services.Common;
using OpenPay.Services.Models;

namespace OpenPay.Services;
public class ItemService : BaseService<Item, ItemDTO, ItemDataDTO>, IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger, IUnitOfWork unitOfWork)
        : base(itemRepository, logger, unitOfWork)
    {
        _itemRepository = itemRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async IAsyncEnumerable<ItemDTO> GetAllAsync()
    {
        await foreach (var itemData in _itemRepository.GetAllAsync())
        {
            var item = Item.FromDataDTO(itemData);
            yield return await item.ToDTOAsync();
        }
    }

    public async Task<Optional<ItemDTO>> CreateAsync(string name, decimal price, decimal taxPercentage)
    {
        var existsOptional = await _itemRepository.NameExistsAsync(name);

        if (existsOptional.IsInvalid) return (Exception)existsOptional;

        if (existsOptional.Handle(exists => exists, _ => false))
        {
            return new BadRequestException("Item with that name already in system.");
        }

        Item item = Item.Create(name, price, taxPercentage);
        var itemOptional = await _itemRepository.CreateAsync(item.ToDataDTO());

        return await itemOptional.HandleAsync(async data =>
        {
            await _unitOfWork.SaveChangesAsync();
            Item item = Item.FromDataDTO(data);
            return new Optional<ItemDTO>(await item.ToDTOAsync());
        }, ex =>
        {
            return ex;
        });
    }

    public async Task<Optional<ItemDTO>> EditAsync(Guid id, string? name, decimal? price, decimal? taxPercentage)
    {
        var existsOptional = await _itemRepository.IdExistsAsync(id);

        if (existsOptional.IsInvalid) return (Exception)existsOptional;

        if (existsOptional.Handle(exists => !exists, _ => false))
        {
            return new NotFoundException("Item with that Guid could not be found.");
        }

        if (name != null)
        {
            var nameExistsOptional = await _itemRepository.NameExistsAsync(name);

            if (nameExistsOptional.IsInvalid) return (Exception)existsOptional;

            if (nameExistsOptional.Handle(exists => exists, _ => false))
            {
                return new BadRequestException("Item with that name already in system.");
            }
        }

        var itemData = await _itemRepository.GetByIdAsync(id);

        return await itemData.HandleAsync(async data =>
        {
            var newItemData = await _itemRepository.EditAsync(id, new EditItemDataDTO
            {
                Name = name,
                Price = price,
                TaxPercentage = taxPercentage
            });

            return await newItemData.HandleAsync(async data =>
            {
                await _unitOfWork.SaveChangesAsync();
                Item item = Item.FromDataDTO(data);
                return new Optional<ItemDTO>(await item.ToDTOAsync());
            }, ex => ex);
        }, ex => ex);
    }

    public override Item FromDataDTO(ItemDataDTO dataDTO)
    {
        return Item.FromDataDTO(dataDTO);
    }
}