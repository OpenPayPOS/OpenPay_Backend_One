
namespace OpenPay.Interfaces.Data.Models;

public interface IUnitOfWork
{
    public int SaveChanges();
    Task<int> SaveChangesAsync();
    Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
