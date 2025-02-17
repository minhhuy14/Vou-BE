using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shared.Repositories;

public abstract class GenericUnitOfWork<TDbContext> : IGenericUnitOfWork, IAsyncDisposable where TDbContext: DbContext
{
    protected readonly TDbContext DbContext;
    protected IDbContextTransaction? Transaction;
    public GenericUnitOfWork(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> OpenTransactionAsync(CancellationToken cancellationToken = default)
    {
        Transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
        return Transaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Transaction is not null)
        {
            await Transaction.CommitAsync(cancellationToken);
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Transaction is not null)
        {
            await Transaction.RollbackAsync(cancellationToken);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (Transaction is not null)
        {
            await Transaction.DisposeAsync();
        }
        await DbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}