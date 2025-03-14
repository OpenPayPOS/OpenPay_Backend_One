using OpenPay.Interfaces.Data.DataModels.Item;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Services.Models;
public class Item : BaseModel<Item, ItemDTO, ItemDataDTO>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public decimal TaxPercentage { get; private set; }
    public string ImagePath { get; }

    private Item(Guid id, string name, decimal price, decimal taxPercentage, string imagePath)
    {
        Id = id;
        Name = name;
        Price = price;
        TaxPercentage = taxPercentage;
        ImagePath = imagePath;
    }

    public static Item Create(string name, decimal price, decimal taxPercentage, string imagePath)
    {
        return new Item(Guid.NewGuid(), name, price, taxPercentage, imagePath);
    }

    public static Item FromDataDTO(ItemDataDTO dataDTO)
    {
        return new Item(dataDTO.Id, dataDTO.Name, dataDTO.Price, dataDTO.TaxPercentage, dataDTO.ImagePath);
    }

    public override ItemDataDTO ToDataDTO()
    {
        return new ItemDataDTO
        {
            Id = Id,
            Name = Name,
            Price = Price,
            TaxPercentage = TaxPercentage,
            ImagePath = ImagePath
        };
    }

    public override Task<ItemDTO> ToDTOAsync()
    {
        return Task.FromResult(new ItemDTO
        {
            Id = Id,
            Name = Name,
            Price = Price,
            TaxPercentage = TaxPercentage,
            ImagePath = ImagePath
        });
    }
}