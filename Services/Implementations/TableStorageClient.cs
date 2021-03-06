using System.Runtime.InteropServices.ComTypes;
using ABU.Portfolio.Services.Abstractions;
using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Services.Implementations;

public class TableStorageClient : ITableStorageClient
{
    private const string AzureConnectionString = "AZURE_STORAGE_CS";
    private readonly CloudTableClient _client;

    public TableStorageClient()
    {
        var connectionString = Environment.GetEnvironmentVariable(AzureConnectionString);
        var storageAccount = CloudStorageAccount.Parse(
            Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString)));

        _client = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
    }

    public async Task<ITableEntity?> ExecuteTableOperationAsync(
        string tableName, 
        TableOperation operation, 
        CancellationToken ct = default)
    {
        var table = await GetCloudTableAsync(tableName, ct);
        var tableResult = await table.ExecuteAsync(operation, ct);
        return tableResult.Result as ITableEntity;
    }

    public async Task<IEnumerable<ITableEntity?>> ExecuteTableBatchOperationAsync(
        string tableName,
        TableBatchOperation batchOperation,
        CancellationToken ct = default)
    {
        var table = await GetCloudTableAsync(tableName, ct);
        var tableBatchResult = await table.ExecuteBatchAsync(batchOperation, ct);

        return tableBatchResult.Select(tableResult => tableResult.Result as ITableEntity)
            .ToList();
    }

    public async Task<CloudTable> GetCloudTableAsync(
        string tableName, 
        CancellationToken ct = default)
    {
        var table = _client.GetTableReference(tableName);
        _ = await table.CreateIfNotExistsAsync(ct);

        return table;
    }
}