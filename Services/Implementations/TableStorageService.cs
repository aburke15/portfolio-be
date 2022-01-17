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

    public async Task<TableBatchResult> InsertManyAsync(string tableName, IEnumerable<ITableEntity> entities, CancellationToken ct = default)
    {
        var table = await _client.GetCloudTableAsync(tableName, ct);
        TableBatchOperation batchOperation = new();

        foreach (var tableEntity in entities)
        {
            batchOperation.InsertOrMerge(tableEntity);
        }

        return await table.ExecuteBatchAsync(batchOperation, ct);
    }

    public async Task<ITableEntity?> RetrieveAsync(string tableName, string id, string partitionKey, CancellationToken ct = default)
    {
        var retrieve = TableOperation.Retrieve<ITableEntity>(id, partitionKey);
        return await _client.ExecuteTableOperationAsync(tableName, retrieve, ct);
    }

    public async Task<IEnumerable<ITableEntity>> RetrieveAllAsync(string tableName, CancellationToken ct = default)
    {
        var table = await _client.GetCloudTableAsync(tableName, ct);
        var query = from entity in table.CreateQuery<TableEntity>() select entity;

        return query.ToList();
    }
}