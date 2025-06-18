using Microsoft.EntityFrameworkCore;
using OpenPay.Data.DataModels;

namespace OpenPay.Data.Repositories;

public class FinancesRepository
{

    AppDbContext _dbContext;

    public FinancesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public async FinancesDataModel GetFinances(DateTime? startTime, DateTime? endTime)
    //{
    //    decimal totalSpent = _dbContext.Orders.Where(o => (startTime == null || o.OrderPlaced >= startTime) && (endTime == null || o.OrderPlaced <= endTime))
    //        .Include(o => o.OrderItems)
    //        .ThenInclude(i => i.Item)
    //        .SelectMany(o => o.OrderItems.Select(i => i.Amount * i.Item.Price))
    //        .Sum();

        
    //}
}
