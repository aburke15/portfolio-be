using ABU.Portfolio.Models;
using ABU.Portfolio.Services.Abstractions;
using Ardalis.GuardClauses;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Services.Implementations;

public class TableStorageService : ITableStorageService
{
    private readonly ITableStorageClient _client;
    
    public TableStorageService(ITableStorageClient client)
    {
        _client = Guard.Against.Null(client, nameof(client));
    }
    
    public async Task<ITableEntity?> DeleteAsync(string tableName, ITableEntity entity, CancellationToken ct = default)
    {
        var delete = TableOperation.Delete(entity);
        return await _client.ExecuteTableOperationAsync(tableName, delete, ct);
    }

    public async Task<ITableEntity?> InsertOrMergeAsync(string tableName, ITableEntity entity, CancellationToken ct = default)
    {
        var insertOrMerge = TableOperation.InsertOrMerge(entity);
        return await _client.ExecuteTableOperationAsync(tableName, insertOrMerge, ct);
    }

    public async Task<IEnumerable<ITableEntity?>> InsertManyAsync(string tableName, IEnumerable<ITableEntity> entities, CancellationToken ct = default)
    {
        TableBatchOperation batchOperation = new();

        foreach (var tableEntity in entities)
        {
            batchOperation.InsertOrMerge(tableEntity);
        }

        return await _client.ExecuteTableBatchOperationAsync(tableName, batchOperation, ct);
    }
}