using OpenPay.Interfaces.Data.Models;

namespace OpenPay.Data.Models;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int SaveChanges() => _dbContext.SaveChanges();

    public async Task<int> SaveChangesAsync()
    {
        _dbContext.ChangeTracker.DetectChanges();
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        return new Transaction(transaction);
    }
}