using OpenPay.Interfaces.Data.DataModels;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Services.Models;
public class Item : BaseModel<Item, ItemDTO, ItemDataDTO>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public decimal TaxPercentage { get; private set; }

    private Item(Guid id, string name, decimal price, decimal taxPercentage)
    {
        Id = id;
        Name = name;
        Price = price;
        TaxPercentage = taxPercentage;
    }

    public static Item Create(string name, decimal price, decimal taxPercentage)
    {
        return new Item(Guid.NewGuid(), name, price, taxPercentage);
    }

    public static Item FromDataDTO(ItemDataDTO dataDTO)
    {
        return new Item(dataDTO.Id, dataDTO.Name, dataDTO.Price, dataDTO.TaxPercentage);
    }

    public override ItemDataDTO ToDataDTO()
    {
        return new ItemDataDTO
        {
            Id = Id,
            Name = Name,
            Price = Price,
            TaxPercentage = TaxPercentage
        };
    }

    public override Task<ItemDTO> ToDTOAsync()
    {
        return Task.FromResult(new ItemDTO
        {
            Id = Id,
            Name = Name,
            Price = Price,
            TaxPercentage = TaxPercentage
        });
    }
}