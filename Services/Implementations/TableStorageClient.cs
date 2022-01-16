using System.Runtime.InteropServices.ComTypes;
using ABU.Portfolio.Services.Abstractions;
using Ardalis.GuardClauses;
using Microsoft.Azure.Cosmos.Table;

namespace ABU.Portfolio.Services.Implementations;

public class TableStorageClient : ITableStorageClient
{
    private const string AzureConnectionString = "DOTNET_AZURE_CONNECTION_STRING";
    private readonly CloudTableClient _client;
    
    public TableStorageClient()
    {
        var connectionString = Environment.GetEnvironmentVariable(AzureConnectionString);
        var storageAccount = CloudStorageAccount.Parse(Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString)));
        _client = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
    }
    
    public async Task<object> ExecuteTableOperationAsync(string tableName, TableOperation operation, CancellationToken ct = default)
    {
        var table = await GetCloudTableAsync(tableName, ct);
        var tableResult = await table.ExecuteAsync(operation, ct);
        return tableResult.Result;
    }

    private async Task<CloudTable> GetCloudTableAsync(string tableName, CancellationToken ct = default)
    {
        var table = _client.GetTableReference(tableName);
        _ = await table.CreateIfNotExistsAsync(ct);

        return table;
    }
}