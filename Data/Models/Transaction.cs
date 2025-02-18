using Microsoft.EntityFrameworkCore.Storage;
using OpenPay.Interfaces.Data.Models;

namespace OpenPay.Data.Models;
public class Transaction : ITransaction
{
    private readonly IDbContextTransaction _transaction;

    public Transaction(IDbContextTransaction transaction)
    {
        _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        _transaction.Dispose();
    }
}