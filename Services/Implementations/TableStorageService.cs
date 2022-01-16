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
        return await _client.ExecuteTableOperationAsync(tableName, delete, ct) as ITableEntity;
    }

    public async Task<ITableEntity?> InsertOrMergeAsync(string tableName, ITableEntity entity, CancellationToken ct = default)
    {
        var insertOrMerge = TableOperation.InsertOrMerge(entity);
        return await _client.ExecuteTableOperationAsync(tableName, insertOrMerge, ct) as ITableEntity;
    }

    public async Task<ITableEntity?> RetrieveAsync(string tableName, string id, string partitionKey, CancellationToken ct = default)
    {
        var retrieve = TableOperation.Retrieve<ITableEntity>(id, partitionKey);
        return await _client.ExecuteTableOperationAsync(tableName, retrieve, ct) as ITableEntity;
    }
}