using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Services.Abstractions;

public interface ITableStorageService
{
    Task<ITableEntity?> DeleteAsync(string tableName, ITableEntity entity, CancellationToken ct = default);
    Task<ITableEntity?> InsertOrMergeAsync(string tableName, ITableEntity entity, CancellationToken ct = default);
    Task<IEnumerable<ITableEntity?>> InsertManyAsync(string tableName, IEnumerable<ITableEntity> entities, CancellationToken ct = default);
}