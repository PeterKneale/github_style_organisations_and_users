using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Demo.Core.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}
public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _db;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DatabaseContext db)
    {
        _db = db;
    }
    
    public async Task BeginTransactionAsync()
    {
        _transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _db.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
        }
    }

    private void RollbackTransaction()
    {
        try
        {
            _transaction.Rollback();
        }
        finally
        {
            _transaction?.Dispose();
        }
    }
}
