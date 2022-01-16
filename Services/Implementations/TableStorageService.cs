using ABU.Portfolio.Services.Abstractions;

namespace ABU.Portfolio.Services.Implementations;

public class TableStorageService : ITableStorageService
{
    public async Task<TEntity> DeleteAsync<TEntity>(string table, TEntity entity, CancellationToken ct = default) where TEntity : class
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> InsertOrMergeAsync<TEntity>(string table, TEntity entity, CancellationToken ct = default) where TEntity : class
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> RetrieveAsync<TEntity>(string table, string id, string partitionKey, CancellationToken ct = default) where TEntity : class
    {
        throw new NotImplementedException();
    }
}